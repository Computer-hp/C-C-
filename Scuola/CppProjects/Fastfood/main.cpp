#include <iostream>
#include <mutex>
#include <condition_variable>
#include <thread>
#include <queue>

#define TEMPO_INSERIMENTO  21
#define TEMPO_PRELEVAMENTO 56 

std::condition_variable_any queue_full, queue_empty;

std::mutex spillone_mutex, queue_full_mutex, queue_empty_mutex;

std::atomic<int> queue_count = 0;

void inserisciOrdine();
void prelevaOrdine();


typedef struct
{
    int numero;
    char* persona;
} ordine;


class Spillone
{
private:
    std::queue<ordine> ordini;


public:
    void setOrdine(ordine* ordine);
    ordine getOrdine();
    int getNumeroOrdini(); 
    
};






void inserisciOrdine(Spillone *spillone)
{
    while (true)
    {
        if (queue)
        ordine o = { .numero = 3; .persona = "Paolo"; }
        spillone -> ordini.push(o);

        srand(time(NULL));
        int random_time = rand() % TEMPO_INSERIMENTO + 10;

        std::this_thread::sleep_for(std::chrono::milliseconds(random_time));
    }
}


void prelevaOrdine()
{
    while (true)
    {
        
        srand(time(NULL));
        int random_time = rand() % TEMPO_INSERIMENTO + 5;

        std::this_thread::sleep_for(std::chrono::milliseconds(random_time));
    }
}


int main()
{
    thread cassiere(inserisciOrdine);
    thread cuoco(prelevaOrdine);

    return 0;
}


