#include <iostream>
#include <thread>
#include <mutex>
#include <vector>
#include <ctime>
#include <fstream>
#include <string>
#include <condition_variable>

#define N_DRONES 8

void initialize_data();
void initialize_drones();
int get_drone_data(float *lat, float *lon, float *alt);

void change_drone_position();

void initialize_reading_threads();

void write();
void read(int i);



std::condition_variable_any drones_vector_is_occupied;
std::condition_variable_any drone_data_vector_is_occupied;
bool new_data_available = false; 

std::mutex critical;


typedef struct
{
	float latitude;
	float longitude;
	float altitude;
} Data;


class Drone
{
private:
	float latitude;
	float longitude;
	float altitude;

public:
	Drone() 
	{ 
		latitude = 0; 
		longitude = 0; 
		altitude = 0; 
	}

	Drone(float *lat, float *lon, float *alt)
	{
		set_position(lat, lon, alt);
	}

	float get_latitude()  const { return latitude; }
	float get_longitude() const { return longitude; }
	float get_altitude()  const { return altitude; }


	void set_position(float *lat, float *lon, float *alt)
	{
		latitude  = (*lat < 0) ? 0 : *lat;
		longitude = (*lon < 0) ? 0 : *lon;
		altitude  = (*alt < 0) ? 0 : *alt;
	}
};


void generate_random_position(Data *data);

std::vector<Drone> drones_vector (N_DRONES);
std::vector<Data> drone_data_vector (N_DRONES);


int get_drone_data(float *lat, float *lon, float *alt)
{
	if (*lat < 0 || *lon < 0 || *alt < 0)
		return -1;

	int drone_position = 0;

	critical.lock();

		for (Drone drone : drones_vector)
		{
			if (*lat == drone.get_latitude() && 
				*lon < drone.get_longitude() && 
				*alt < drone.get_altitude())
			
				return drone_position; 	

			drone_position++;
		}

	critical.unlock();

	return -1;
}


void change_drone_position()
{
	srand(time(NULL));

	while (true)
	{
		Data data;
		generate_random_position(&data);

		int i = rand() % N_DRONES;

		critical.lock();

			drones_vector[i].set_position(&data.latitude, &data.longitude, &data.altitude);

		critical.unlock();

		std::this_thread::sleep_for(std::chrono::seconds(2));
	}
}


void write()
{
	srand(time(NULL));

	while (true)	
	{
		Data data;
		generate_random_position(&data);

		if (int i = get_drone_data(&data.latitude, &data.longitude, &data.altitude) != -1)
		{
			critical.lock();

				drone_data_vector[i] = data;

			critical.unlock();
		}

		std::this_thread::sleep_for(std::chrono::seconds(1));
	}
}


void read(int i)
{
	std::string file_name = "file" + std::to_string(i) + ".txt";
	std::fstream drone_file(file_name, std::ios::out);
	drone_file << "Drone " << std::to_string(i) << std::endl;

	while (true)
	{
		critical.lock();

			drone_file << std::endl;
			drone_file << "Latitude: " << drone_data_vector[i].latitude << std::endl;
			drone_file << "Longitude: " << drone_data_vector[i].longitude << std::endl;
			drone_file << "Altitude: " << drone_data_vector[i].altitude << std::endl;

		critical.unlock();	

		std::this_thread::sleep_for(std::chrono::seconds(1 / 2));
	}
}


void initialize_reading_threads()
{
	for (int i = 0; i < N_DRONES; i++)
		std::thread(read, i).detach();
}


void initialize_drones()
{
	srand(time(NULL));

	for (int i = 0; i < N_DRONES; i++)
	{
		Data data;
		generate_random_position(&data);
		drones_vector.push_back(Drone(&data.latitude, &data.longitude, &data.altitude));
	}
}


void generate_random_position(Data *data)
{
	data -> latitude = rand() % 100;	
	data -> longitude = rand() % 100;	
	data -> altitude = rand() % 100;	
}


void initialize_drone_data()
{
	for (int i = 0; i < N_DRONES; i++)
	{
		const auto drone = drones_vector[i];

		Data data = 
		{ 
			.latitude = drone.get_latitude(), 
			.longitude = drone.get_longitude(), 
			.altitude = drone.get_altitude()
		};

		drone_data_vector.push_back(data);
	}
}


int main()
{
	initialize_drones();	
	initialize_drone_data();

	std::thread writing_thread(write);

	initialize_reading_threads();

	std::thread change_drone_position_thread(change_drone_position);


	writing_thread.join();
	change_drone_position_thread.join();
}