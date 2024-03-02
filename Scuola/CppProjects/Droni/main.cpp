#include <iostream>
#include <thread>
#include <atomic>
#include <mutex>
#include <vector>
#include <list>


#define N_DRONES 8

std::vector<list<DDrrone>> drones_vector (N_DRONES);
void add_drone_to_vector(Drone *drone);
int get_drone_data(float *lat, float *lon, float *alt);


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
		setPosition(&lat, &lon, &alt);
	}

	float get_latitude()  const { return latitude; }
	float get_longitude() const { return longitude; }
	float get_altitude()  const { return altitude; }


	void set_position(float *lat, float *lon, float *alt) const
	{
		latitude  = (lat < 0) ? 0 : lat;
		longitude = (lon < 0) ? 0 : lon;
		altitude  = (alt < 0) ? 0 : alt;
	}
};


void add_drone_to_vector(Drone *drone)
{
	
}
