#include <bits/stdc++.h>
#include "Data.cpp"

#define N_ROBOT 5

using namespace std::chrono;


std::atomic<int> active_threads = N_ROBOT + 1;


const int MIN = -100;
const int MAX = 100;

std::vector<Data> vector_of_data(N_ROBOT);

std::mutex critical;
std::condition_variable_any wait_for_vector_of_data;
bool vector_of_data_is_occupied = false;



void initialize_robots();
void base();
void read_data(int robot_number);
Data generate_random_data();



int main()
{
    srand(time(NULL));

    initialize_robots();

    std::thread base_thread(base);

    std::this_thread::sleep_for(seconds(2));

    for (int i = 0; i < N_ROBOT; i++)
        std::thread(read_data, i).detach();

    while (active_threads > 0);

    base_thread.join();

    return 0;
}


void initialize_robots()
{
    for (int i = 0; i < N_ROBOT; i++)
        vector_of_data[i] = generate_random_data();

    std::cout << "Numero Robot \t X \t Y \t Battery \t Speed";
}


void base()
{
    while (true)
    {
        std::this_thread::sleep_for(milliseconds(750)); 

        Data data = generate_random_data();

        int random_index = rand() % N_ROBOT;

        {
            std::lock_guard<std::mutex> lock(critical);

            if (vector_of_data_is_occupied)
                wait_for_vector_of_data.wait_for(critical, seconds(250));

            vector_of_data_is_occupied = true;

            vector_of_data[random_index] = data;

            vector_of_data_is_occupied = false;
        }

        wait_for_vector_of_data.notify_one();
    }

    active_threads--;
}


Data generate_random_data()
{
    Data tmp =
    {
        .x = rand() % (MAX * 2 + 1) - MIN,
        .y = rand() % (MAX * 2 + 1) - MIN,
        .battery_level = rand() % 100,
        .speed = (float)rand() / ((float)RAND_MAX / float(MAX)),
        .changed_data = true
    };

    return tmp;
}


void read_data(int robot_number)
{
    while (true)
    {
        std::this_thread::sleep_for(milliseconds(500)); 

        {
            std::lock_guard<std::mutex> lock(critical);

            if (vector_of_data_is_occupied)
                wait_for_vector_of_data.wait_for(critical, milliseconds(375));

            vector_of_data_is_occupied = true;

            if (!vector_of_data[robot_number].changed_data)
            {
                wait_for_vector_of_data.notify_one();
                continue;
            }

            std::cout << '\n'
                      << robot_number << "\t\t" 
                      << vector_of_data[robot_number].x << "\t" 
                      << vector_of_data[robot_number].y << "\t  " 
                      << vector_of_data[robot_number].battery_level << "\t\t" 
                      << vector_of_data[robot_number].speed;

            vector_of_data[robot_number].changed_data = false;

            vector_of_data_is_occupied = false;
        }

        wait_for_vector_of_data.notify_one();
    }

    active_threads--;
}