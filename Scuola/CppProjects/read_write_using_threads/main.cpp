#include <iostream>


const std::string IN_FILE 	=	"nazioni1.txt";
const std::string OUT_FILE	= 	"nazioni2.txt";

int main()
{
	in_file = fopen(IN_FILE, "r");

	if (!in_file)
	{
		std::cout << "\nError occured when oppening the file."
		return -1;
	}

	out_file = fopen(OUT_FILE, "w");

	if (!out_file)
	{
		std::cout << "\nError occured when oppening the file.";
		return -1;
	}



	return 0;
}
