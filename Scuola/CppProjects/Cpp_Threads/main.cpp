#include <iostream>
#include <thread>

void sum(int a, int b)
{
	std::cout << "a + b = " << a + b << std::endl;
}


void sub(int a, int b)
{
	std::cout << "a - b = " << a - b << std::endl;
}

void print(std::string str)
{
	std::cout << str << std::endl;
}


int main()
{
	int a = 5, b = 2;
	std::string str = "End";

	std::thread t1(sum, a, b);
	std::thread t2(sub, a, b);
	std::thread t3(print, str);

	t1.join();
	t2.join();
	t3.join();
}
