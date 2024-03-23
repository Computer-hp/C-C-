#include <bits/stdc++.h>
#include "Data.cpp"

#define N_ROBOT 5

using namespace std::chrono;

const int MIN = -100;
const int MAX = 100;

std::vector<Data> vector_of_data;
std::mutex critical;
std::condition_variable_any wait_for_vector_of_data;
bool vector_of_data_is_occupied = false;


void base();
void read_data();
Data generate_random_data();


int main()
{
    std::thread base_thread(base);

    for (int i = 0; i < N_ROBOT; i++)
        std::thread(read_data).detach();

    return 0;
}


void base()
{
    srand(time(NULL));

    while (true)
    {
        std::this_thread::sleep_for(seconds(1));

        Data data = generate_random_data();

        {
            std::lock_guard<std::mutex> lock(critical);

            if (vector_of_data_is_occupied)
                wait_for_vector_of_data.wait(critical);

            vector_of_data_is_occupied = true;

            vector_of_data.push_back(data);


            vector_of_data_is_occupied = false;
        }

        wait_for_vector_of_data.notify_one();
    }
}


Data generate_random_data()
{
    Data tmp;

    tmp.x = rand() % (MAX * 2 + 1) - MIN;
    tmp.y = rand() % (MAX * 2 + 1) - MIN;  
    tmp.battery_level = rand() % 100;
    tmp.speed = (float)rand() / ((float)RAND_MAX / float(MAX));

    return tmp;
}


void read_data()
{

}