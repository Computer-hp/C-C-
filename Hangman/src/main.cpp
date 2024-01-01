#include <iostream>
#include <limits>
#include <fstream>
#include <string>
#include <filesystem>

#define USERNAME_MAX_LENGTH 12

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
    void get_credentials_from_user_input(void);
    void handle_credentials(void);
    void menu(void);
    char menu_user_input(void);
    bool check_character_from_input(char &input_character);
    void play(void);
    void see_score(void);
    void exit(void);
};


/*
    Controls if the character taken 
    from the input is valid or not.
*/

bool Game::check_character_from_input(char &input_character)
{
    std::cin >> input_character;
    std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');

    if (std::cin.fail())
    {
        std::cin.clear(); // Clear the fail state
        std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n'); // Clear the buffer

        std::cout << "Invalid input. Insert a character" << std::endl;
        return false;
    }
    return true;
}


/*
    Returns the character
    which is used to start playing the game,
    to see the score or to exit the game.
*/

char Game::menu_user_input(void)
{
    char input_character;

    while (true)
    {
        printf("\n\n\t\tPlay\n\n\t\tScore\n\n\t\tExit");
        printf("Type:\t'p' to Play\t's' to see the Score\t'e' to Exit\n");

        if (check_character_from_input(input_character))
            if (input_character == 'p' || 
                input_character == 's' || 
                input_character == 'e')
                
                break;

    }

    return input_character;
}

void Game::play(void)
{

}

void Game::see_score(void)
{
    std::cout << '\n' << this->username << "\tCurrent score: " << this->score;

    char input_character;

    while (true)
    {
        std::cout << "\nPress 'e' to exit: ";
        
        if (check_character_from_input(input_character) && input_character != 'e')
            break;
    }

    // return to menu
}

void Game::exit(void)
{

}

// TODO after visualizing or finishing the game, add
// the possibility to return to the main menu.

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

void Game::get_credentials_from_user_input(void)
{
    std::string input_username;
    
    do
    {
        char input_string[USERNAME_MAX_LENGTH + 1];

        std::cout << "\n\t Enter you name: ";
        std::cin.getline(input_string, USERNAME_MAX_LENGTH + 1);

        input_username = input_string;

        if (input_username.length() <= USERNAME_MAX_LENGTH) 
            break;

    } while (true);

    this->username = input_username;
}

/*
    Creates the directory Hangman Credentials, if doesn't exists.
    Creates the directory credentials.txt, if doesn't exists.
    Controls whether username exists if there is already credentials.txt.
*/

void Game::handle_credentials(void)
{
    const std::string exe_path = std::filesystem::current_path();

    const std::string credentials_dir_name = exe_path + "Hangman Player Credentials";
    const std::string credentials_file_name = "credentials.txt";
    
    if (!std::filesystem::exists(credentials_dir_name))
    {
        if (!std::filesystem::create_directory(credentials_dir_name))
        {
            std::cout << "\nError occured while creating credentials directory\n";
            return;
        }
    }

    if (!std::filesystem::exists(credentials_file_name))
    {
        std::ofstream credentials_file(credentials_file_name);

        if (!credentials_file)
        {
            std::cout << "\nError occured while creating credentials file\n";
            return;
        }
        credentials_file.close();
    }

    std::fstream credentials_file(credentials_file_name);

    if (!credentials_file)
    {
        std::cout << "\nError occured while opening the credentials file\n";
        return;
    }

    std::string line;

    while (std::getline(credentials_file, line)) // if username already exists
    {
        std::string first_word = line.substr(0, line.find_first_of(" "));

        if (first_word.compare(this->username) == 0)
        {
            size_t score_position = line.find_last_of(" ");

            this->score = std::stoi(line.substr(score_position + 1));

            credentials_file.close();
            return;
        }
    }
    
    credentials_file << this->username << " score: " << this->score;
    credentials_file.close();    
}

void Game::start(void)
{
    get_credentials_from_user_input();
    handle_credentials();
    menu();
}


int main()
{
    Game game;

    game.start();

    return 0;
}
