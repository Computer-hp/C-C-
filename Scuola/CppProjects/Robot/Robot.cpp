#include <iostream>
#include "Data.cpp"

class Robot
{
private:

    Data data;

public:

    Robot() { }

    Robot(int x, int y, int battery_level, float speed)
    {
        set_data(x, y, battery_level, speed);
    }

    Data get_data() const { return data; }

    void set_data(int x, int y, int battery_level, float speed);
};