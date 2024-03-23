#include <bits/stdc++.h>

#define N_CHAR   8 
#define IN_FILE  "input.txt"
#define OUT_FILE "output.txt"

void initialize_files();

void read_file();
void elaborate_file();
void write_file();

void DES_encode(char *input, const char key);
void DES_decode(char *input, const char key);

bool input_file_is_empty();
int array_size(const char *str);

std::mutex critical, DES_critical;
std::condition_variable_any wait_for_in_queue, wait_for_out_queue; 
std::queue<std::array<char, N_CHAR>> in_words, out_words;

bool in_queue_is_occupied = false;
bool out_queue_is_occupied = false;

std::atomic<int> active_threads = 3;

int in_file_size = 0;


int main()
{
    initialize_files();

    std::thread read(read_file);
    std::thread elaborate(elaborate_file);
    std::thread write(write_file);

    while (active_threads > 0);

    read.join();
    elaborate.join();
    write.join();

    return 0;
}


void read_file()
{
    int counter = 0;
    int current_position = 0;

    while (counter < in_file_size)
    {
        char ch;
        std::array<char, N_CHAR> char_array{};

        std::this_thread::sleep_for(std::chrono::milliseconds(400));

	{
	    std::lock_guard<std::mutex> lck(critical);

	    if (in_queue_is_occupied)
		wait_for_in_queue.wait(critical);

	    in_queue_is_occupied = true;

	    std::ifstream in_file(IN_FILE);

	    if (!in_file)
	    {
		std::cout << "\nError occured while opening " << IN_FILE << std::endl;
		break;
	    }

	    in_file.seekg(current_position);

            for (int i = 0; i < N_CHAR && counter < in_file_size; i++)
            {
                in_file.get(ch);

                if (ch == '\n')
                    in_file.get(ch);

                char_array[i] = ch;
                counter++;
            }

	    in_words.push(char_array);

            std::cout << "\n\tin_words.back() = " << in_words.back().data() << '\n';

            std::cout << "\n\t\tin_current_position = " << counter << '\n';
	    
	    current_position = in_file.tellg();

            in_file.close();

	    in_queue_is_occupied = false;
	}
		
	wait_for_in_queue.notify_one();
    }

    in_queue_is_occupied = false;
    wait_for_in_queue.notify_one();

    std::cout << "\nRead thread has finished\n";
    active_threads--;
}


void elaborate_file()
{
    int counter = 0;
    
    while (counter < in_file_size)
    {
        std::this_thread::sleep_for(std::chrono::milliseconds(500));

        char str[N_CHAR + 1];

        {
            std::lock_guard<std::mutex> lck(critical);

            if (in_queue_is_occupied)
                wait_for_in_queue.wait(critical);

            in_queue_is_occupied = true;

            std::copy(in_words.front().begin(), in_words.front().end(), str);

            in_words.pop();

            in_queue_is_occupied = false;
        }

        wait_for_in_queue.notify_one();

	std::cout << "\nelaborate_str = " << str << '\n';
        counter += array_size(str);
        DES_encode(str, 0x0F);
    }

    in_queue_is_occupied = false;
    wait_for_in_queue.notify_one();

    std::cout << "\nElaborate thread has finished\n";
    active_threads--;
}


void write_file()
{
    int counter = 0;

    while (counter < in_file_size)
    {
        std::this_thread::sleep_for(std::chrono::milliseconds(800));

        {
            std::lock_guard<std::mutex> lck(critical);
            
            if (out_queue_is_occupied)
                wait_for_out_queue.wait(critical);

            out_queue_is_occupied = true;

            std::ofstream out_file(OUT_FILE, std::ios::app);

            if (!out_file)
            {
                std::cout << "\nError occured while opening " << OUT_FILE << std::endl;
                break;
            }

            out_file << out_words.front().data() << '\n';
            out_file.close();

	    std::cout << "\nout_words.front().data() = " << out_words.front().data() << '\n';
            counter = array_size(&out_words.front()[0]);

            out_words.pop();

            out_queue_is_occupied = false;
        }
	
        wait_for_out_queue.notify_one();
        std::cout << "\nout_current_position = " << counter << '\n';
    }

    out_queue_is_occupied = false;
    wait_for_out_queue.notify_one();

    std::cout << "\nWrite thread has finished\n";
    active_threads--;
}


void DES_encode(char *input, const char key)
{
    std::array<char, N_CHAR> char_array{};

    DES_critical.lock();

        if (out_queue_is_occupied)
            wait_for_out_queue.wait(critical);

        out_queue_is_occupied = true;

        while (*input)
            *(input++) ^= key;

        std::copy(input, input + std::strlen(input) + 1, char_array.begin());
        out_words.push(char_array);

    	out_queue_is_occupied = false;

    DES_critical.unlock();

    wait_for_out_queue.notify_one();
}


void DES_decode(char *input, const char key)
{
    std::array<char, N_CHAR> char_array{};
    
    DES_critical.lock();
        
        if (out_queue_is_occupied)
            wait_for_out_queue.wait(critical);

        out_queue_is_occupied = true;

        while (*input)
            *(input++) ^= key;

        std::copy(input, input + std::strlen(input) + 1, char_array.begin());
        out_words.push(char_array);
	
	out_queue_is_occupied = false;

    DES_critical.unlock();

    wait_for_out_queue.notify_one();
}


bool input_file_is_empty()
{
    std::ifstream file(IN_FILE, std::ios::binary);
    return file.peek() == std::ifstream::traits_type::eof();
}


int array_size(const char *str)
{
    int counter = 0;

    while (*(str++) != '\0')
        counter++;

    std::cout << "\n\t\t\tarray_size = " << strlen(str) << '\n'; 
    std::cout << "\n\t\t\tstr = " << str << '\n';

    return counter;
}


void initialize_files()
{
    std::ofstream in_file(IN_FILE);
    std::ofstream out_file(OUT_FILE, std::ofstream::out | std::ofstream::trunc);
    out_file.close();

    if (input_file_is_empty())
	in_file << "helloworld1helloworld2helloworld3";

    in_file.seekp(0, std::ios::end);
    in_file_size = in_file.tellp();
    in_file.seekp(0);

    in_file.close();
}
