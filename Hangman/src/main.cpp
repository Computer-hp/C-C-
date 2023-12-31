#include <iostream>
#include <limits>
#include <fstream>
#include <string>
#include <filesystem>

/*
class Game -> start method, to start the game

ask username and save it in a file

    play

    score

    exit

play:
    _ _ _ _ _               life = value

-----> type a letter  input

update terminal after player guessed a letter

    _ _ _ _ o

guess --> life++

mistake --> life--

if life <= 0: exit()


player profile:

    create a dir in the path where .exe is located
    then save username, if exists load the score
    delet username --> and score

*/

class Game
{

private:
    std::string username;
    int score;


public:
    void start(void);
    void handle_credentials(std::string username);
    void menu(void);
    char menu_user_input(void);


};

class Hangman
{

};

char Game::menu_user_input(void)
{
    char input_character;

    do
    {
        printf("\n\n\t\tPlay\n\n\t\tScore\n\n\t\tExit");
        printf("Type:\t'p' to Play\t's' to see the Score\t'e' to Exit\n");

        std::cin >> input_character;
        std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');

        if (std::cin.fail())
        {
            std::cin.clear(); // Clear the fail state
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n'); // Clear the buffer

            std::cout << "Invalid input." << std::endl;
        }
        else
            break;

    } while (true);

    return input_character;
}

void Game::menu(void)
{
    char input_character = menu_user_input();

    switch (input_character)
    {
    case 'p':
    case 'P':

        break;

    case 's':
    case 'S':

        break;

    case 'e':
    case 'E':

        break;

    default:
        break;
    }
}

void Game::handle_credentials(std::string username)
{
    const std::string exe_path = std::filesystem::current_path();
    const std::string credentials_dir = exe_path + "credentials";
    const std::string credentials_file = "credentials.txt";
    
    if (std::filesystem::exists(credentials_dir))
    {

    }
    else if (!std::filesystem::create_directory(credentials_dir))
    {
        std::cout << "Error occured while creating credentials directory";
        return;
    }

    if (std::filesystem::exists(credentials_file))

}

void Game::start(void)
{
    std::string username;

    std::cout << "\n\t Enter you name: ";
    std::cin >> username;

    handle_credentials(username);
    
    menu();
}


int main()
{
    

    return 0;
}
