/*
	Example of multithreading in C++ where two type of threads
	have to access a vector that stores data which are shared by
	5 robots (to make things easier the method that stores the data
	in the vector instead of waiting them to arrive, it generates 
	them randomly).
*/


#include <bits/stdc++.h>
#include "Data.cpp"

#define N_ROBOT 5

#define MAX 100
#define MIN -100

#define MAX_SPEED 10
#define MIN_SPEED 0

using namespace std;


vector<Data> vector_of_data(N_ROBOT);

mutex critical;
condition_variable cv;
condition_variable_any cv_any;

bool vector_of_data_is_occupied = false;

atomic<int> working_threads = N_ROBOT + 1;


void initialize_vector_of_data();

void write();
Data generate_random_data();

void read(int n_thread);



int main()
{
	srand(time(NULL));

	initialize_vector_of_data();
	
	thread write_thread(write);

	this_thread::sleep_for(chrono::seconds(2));

	for (int i = 0; i < N_ROBOT; i++)
		thread(read, i).detach();

	while (working_threads > 0);

	write_thread.join();

	return 0;
}


void initialize_vector_of_data()
{
	for (int i = 0; i < N_ROBOT; i++)
		vector_of_data[i] = generate_random_data();

}


void write()
{
	while (true)
	{
		this_thread::sleep_for(chrono::milliseconds(1000));

		Data tmp_data = generate_random_data();

		int i = rand() % N_ROBOT;

		{
			/*
				With condition_variable is not possible to use lock_guard.
				Instead of lock_guard is used unique_lock;


				unique_lock<mutex> lock(critical);

				Waiting till the condition is == false.

					cv.wait(lock, []{ return !vector_of_data_is_occupied; });



				Waiting for 500 milliseconds till the condition is == false, if the time exceedes the method returns false.

					if (!cv.wait_for(lock, chrono::milliseconds(500), []{ return vector_of_data_is_occupied; }))
					{
						cout << "\nThread has stopped waiting";
						continue;
					}
			*/


			/*
				With contidion_variable_any can be used both lock_guard and unique_lock.
			*/

			lock_guard<mutex> lock(critical);

			if (vector_of_data_is_occupied)
				cv_any.wait(critical);

			vector_of_data_is_occupied = true;
			
			vector_of_data[i] = tmp_data;

			vector_of_data_is_occupied = false;
		}
		
	//	cv.notify_one();
		cv_any.notify_one();
	}

	working_threads--;
}



void read(int n_thread)
{
	int i = 0;
	const string file_name = "Robot" + to_string(n_thread) + ".txt";
	
	ofstream out_file(file_name);

	if (!out_file)
	{
		cout << "\nError occured while opening file " << file_name << '\n';
		return;
	}

	out_file << "Numero Robot" << "\t" << "X" << "\t\t" << "Y" << "\t\t" 
		   	 << "Battery" << "\t\t" << "Speed\n";

	out_file.flush();   // neccesarry because otherwise the threads 
						// don't write inside the file.

	while (true)	
	{
		this_thread::sleep_for(chrono::milliseconds(1000));
		
		Data *tmp_data;

		{
			lock_guard<mutex> lock(critical);

			if (vector_of_data_is_occupied)
				cv_any.wait(critical);

			vector_of_data_is_occupied = true;

			if (vector_of_data[n_thread].has_been_read)
			{
				vector_of_data_is_occupied = false;
				cv.notify_one();
				continue;
			}

			tmp_data = &vector_of_data[n_thread];
			tmp_data -> has_been_read = true;

			out_file << "\t" << n_thread << "\t\t\t" 
				 << tmp_data -> x << "\t\t" 
				 << tmp_data -> y << "\t\t"
				 << tmp_data -> battery_level << "\t\t"
				 << tmp_data -> speed << '\n';

			out_file.flush();

			vector_of_data_is_occupied = false;
		}
		
	//	cv.notify_one();
		cv_any.notify_one();
	}

	out_file.close();
	working_threads--;
}


Data generate_random_data()
{
	Data data = 
	{
		.x = rand() % (MAX * 2 + 1) + MIN,
		.y = rand() % (MAX * 2 + 1) + MIN,
		.battery_level = rand() % MAX + 1,
		.speed = (float)rand() / ((float)RAND_MAX / (float)(MAX_SPEED - MIN_SPEED)),
		.has_been_read = false
	};

	return data;
}

