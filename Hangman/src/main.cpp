#include <iostream>
#include <cctype>
#include <cstdlib>
#include <ctime>
#include <fstream>
#include <filesystem>
#include <limits>
#include <string>
#include <vector>

#define LIVES                3
#define USERNAME_MAX_LENGTH  12

const std::filesystem::path program_running_path = std::filesystem::current_path();

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

    int score;
    std::string username;
    std::string word_to_guess;
    std::vector<std::string> words_to_guess;
    std::streampos questions_file_size;

  public:

    Game() { username = ""; score = 0; word_to_guess = "";  set_words_to_vector(); }

    void setScore(int s) 
    { 
        if (s < 0) score = 0;
        
        else       score = s;
    }

    void start(void);
    void get_credentials_from_user_input(void);
    void handle_credentials(void);
    
    void menu(void);
    char menu_user_input(void);

    bool check_character_from_input(char &input_character);
    
    void play(void);
    void set_words_to_vector(void);
    void chose_randomly_word_to_guess(void);
    void handle_guessed_character(char guessed_characters[], char &input_character);
    void print_word_to_be_guessed(char guessed_characters[]);


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
        std::cin.clear();
        std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');

        std::cout << "Invalid input. Insert a character" << std::endl;
        return false;
    }

    if (std::isupper(input_character))
        std::tolower(input_character);

    return true;
}

/*
    Returns the character which is 
    used to start playing the game,
    to see the score or to exit the game.
*/

char Game::menu_user_input(void)
{
    char input_character;

    do
    {
        printf("\n\n\t\tPlay\t\tScore\t\tExit");
        printf("\nType:\n\t\t'p' to Play\t's' to see the Score\t'e' to Exit\n");

        if (check_character_from_input(input_character) && 
            (input_character == 'p' ||
            input_character == 's' || 
            input_character == 'e'))
                
            break;
    }
    while (true);

    return input_character;
}


/*
    Sets the word that has to be guessed 
    in the game by chosing it randomly from 
    a file which contains a list of words.
*/

void Game::chose_randomly_word_to_guess(void)
{
    srand((unsigned) time(NULL));

    std::cout << "\narrived\n";
    int random = rand() % (long)(long)questions_file_size;
    std::cout << "\npassed\n";
    std::cout << "\nrandom = " << random << std::endl;

    word_to_guess = words_to_guess[random];
}

/* 
    Extract's the words from the file questions.txt
    and sets them to a vector, so after the
    'chose_randomly_word_to_guess' method will
    chose from this vector a word that the 
    player will have to guess.
*/

void Game::set_words_to_vector(void)
{
    const std::string questions_dir_name = "questions";
    const std::string questions_file_name = "questions.txt";

    std::string questions_dir_path = program_running_path.parent_path().string() + '/' + questions_dir_name + '/' + questions_file_name;

    std::ifstream questions_file(questions_dir_path, std::ios::in | std::ios::out);

    if (!questions_file)
    {
        std::cout << "\nError occured while creating credentials directory\n";
        return;
    }
    
    std::string word;

    while (questions_file >> word)
    {
        word.resize(word.length() - 1);
        words_to_guess.push_back(word);
    }

    questions_file.seekg(0, std::ios::end);
    std::streampos questions_file_size = questions_file.tellg();

    questions_file.close();

    // DOESNT WORK

    if (questions_file_size == -1)
    {
        std::cout << "Unable to determine file size." << std::endl;
        return;
    }
}

void Game::print_word_to_be_guessed(char guessed_characters[])
{
    std::cout << "\n\t\t";

    for (int i = 0; i < word_to_guess.length(); i++)
        std::cout << guessed_characters[i] << " ";
}

void Game::play(void)
{
    chose_randomly_word_to_guess();

    char guessed_characters[word_to_guess.length()];

    for (int i = 0; i < word_to_guess.length(); i++)
    {
        guessed_characters[i] = '_';
    }

    char input_character;

    while (true)
    {
        print_word_to_be_guessed(guessed_characters);

        std::cout << "\nType a letter: ";
        
        do
        {
            if (check_character_from_input(input_character))
                break;
        }
        while (true);


        if (word_to_guess.find(input_character) == std::string::npos)
            
            setScore(this->score - 1);
        
        else
            handle_guessed_character(guessed_characters, input_character);



    }

    // at the end of the game save the score in credentials.txt

}


void Game::handle_guessed_character(char guessed_characters[], char &input_character)
{
    for (int i = 0; i < word_to_guess.length(); i++)
    {
        if (word_to_guess[i] == input_character)
        {
            guessed_characters[i] = input_character;
        }
    }

    setScore(this->score + 1);
}


void Game::see_score(void)
{
    std::cout << '\n' << this->username << "\tCurrent score: " << this->score;

    char input_character;

    do
    {
        std::cout << "\nPress 'e' to exit: ";
        
        if (check_character_from_input(input_character) && input_character == 'e')
            break;
    } 
    while (true);
    
    menu();
    // return to menu
}


void Game::exit(void)
{
    std::exit(0);
}

// TODO after visualizing or finishing the game, add
// the possibility to return to the main menu.

void Game::menu(void)
{
    char input_character = menu_user_input();

    switch (input_character)
    {
    case 'p':
        play();
        break;

    case 's':
        see_score();
        break;

    case 'e':
        exit();
        break;

    default:
        break;
    }
}

/*
    Takes the username written in input by the user.
    If the username is too long, reasks.
*/

void Game::get_credentials_from_user_input(void)
{
    std::string input_username = "";
    
    do
    {
        char input_string[USERNAME_MAX_LENGTH + 1];

        std::cout << "\n    Enter you name: ";
        std::cin.getline(input_string, USERNAME_MAX_LENGTH + 1);

        input_username = input_string;

        if (input_username.length() <= USERNAME_MAX_LENGTH) 
            break;

    } while (true);

    this->username = input_username;
}

/*
    Creates the directory Hangman Credentials, if doesn't exist.
    Creates the file credentials.txt, if doesn't exist.
    Controls whether username exists (if credentials.txt exists).
*/

void Game::handle_credentials(void)
{
    //const std::string program_running_path = std::filesystem::current_path();

    const std::string credentials_dir_name = "Hangman Player Credentials";
    const std::string credentials_file_name = "credentials.txt";
    
    if (!std::filesystem::exists(credentials_dir_name))
    {
        if (!std::filesystem::create_directory(credentials_dir_name))
        {
            std::cout << "\nError occured while creating credentials directory\n";
            return;
        }
    }

    if (!std::filesystem::exists(credentials_dir_name + "/" + credentials_file_name))
    {
        // TODO create the file inside the 'credentials_dir_name' directory
        std::ofstream credentials_file(credentials_dir_name + "/" + credentials_file_name);

        if (!credentials_file)
        {
            std::cout << "\nError occured while creating credentials file\n";
            return;
        }
        credentials_file.close();
    }

    std::fstream credentials_file(credentials_dir_name + "/" + credentials_file_name, std::ios::in | std::ios::out);

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

    credentials_file.clear();
    credentials_file.seekp(0, std::ios::end);

    credentials_file << this->username << " score: " << this->score << std::endl;
    credentials_file.close();
}

// Starts the Hangman game.

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
