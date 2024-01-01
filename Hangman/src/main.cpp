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
    Game() { username = ""; score = 0; }

    void start(void);
    std::string get_credentials_from_user_input(void);
    void handle_credentials(std::string username);
    void menu(void);
    char menu_user_input(void);
    bool check_character_from_input(char* c);
    void play(void);
    void see_score(void);
    void exit(void);
};


bool Game::check_character_from_input(char* input_character)
{
    std::cin >> input_character;
    std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');

    if (std::cin.fail())
    {
        std::cin.clear(); // Clear the fail state
        std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n'); // Clear the buffer

        std::cout << "Invalid input." << std::endl;

        return false;
    }

    return true;
}

char Game::menu_user_input(void)
{
    char input_character;

    do
    {
        printf("\n\n\t\tPlay\n\n\t\tScore\n\n\t\tExit");
        printf("Type:\t'p' to Play\t's' to see the Score\t'e' to Exit\n");

        if (check_character_from_input(&input_character))
            break;

    } while (true);

    return input_character;
}

void Game::play(void)
{

}

void Game::see_score(void)
{
    std::cout << '\n' << this->username << "\tCurrent score: " << this->score;

    char input_value;
    std::cout << "Press 'e' to exit: ";
    check_character_from_input();
}

void Game::exit(void)
{

}

void Game::menu(void)
{
    char input_character = menu_user_input();

    switch (input_character)
    {
    case 'p':
    case 'P':
        play();
        break;

    case 's':
    case 'S':
        see_score();
        break;

    case 'e':
    case 'E':
        exit();
        break;

    default:
        break;
    }
}

std::string Game::get_credentials_from_user_input(void)
{
    std::string username;
    
    do
    {
        std::cout << "\n\t Enter you name: ";
        std::cin >> username;

        std::cin >> username;
        std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');

        if (std::cin.fail())
        {
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');

            std::cout << "Invalid input." << std::endl;
        }
        else
            break;

    } while (true);

    return username;
}

void Game::handle_credentials(std::string username)
{
    const std::string exe_path = std::filesystem::current_path();
    const std::string credentials_dir = exe_path + "credentials";
    const std::string credentials_file = "credentials.txt";
    
    if (!std::filesystem::exists(credentials_dir))
    {
        if (!std::filesystem::create_directory(credentials_dir))
        {
            std::cout << "Error occured while creating credentials directory";
            return;
        }
    }

    if (!std::filesystem::exists(credentials_file))
    {
        /*
        if (!std::filesystem::create_file(credentials_file))
        {
            std::cout << "Error occured while creating credentials directory";
            return;
        }
        */
    }

    // check if username exists: this->username = username; this->score = found_score;



    // if not write --> username  score: score_initialized_in_the_constructor;


    // in the end
    
}

void Game::start(void)
{
    std::string username = get_credentials_from_user_input();

    handle_credentials(username);
    
    menu();
}


int main()
{
    Game game;

    game.start();

    return 0;
}
