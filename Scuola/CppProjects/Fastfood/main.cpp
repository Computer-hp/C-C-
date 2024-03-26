#include <iostream>
#include <mutex>
#include <condition_variable>
#include <thread>
#include <queue>
#include <ctime>
#include <atomic>

#define TEMPO_INSERIMENTO   3
#define TEMPO_PRELEVAMENTO  8 
#define N_MAX_ORDINI        10


std::condition_variable_any queue_full, queue_empty;

std::mutex critical;

std::atomic<int> numero_ordini = 0;


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
    
    Spillone() { }

    void setOrdine(ordine *o)
    {
        ordini.push(*o);
    }

    ordine getOrdine()
    {
        return ordini.front();
    }

    int getNumeroOrdini()
    {
        return ordini.size();
    }

    void rimuoviPrimoOrdine()
    {
        ordini.pop();
    }
};



void inserisciOrdine(Spillone *spillone)
{
    srand(time(NULL));

    while (true)
    {
        critical.lock();
            
            if (spillone -> getNumeroOrdini() >= N_MAX_ORDINI)
                queue_empty.wait(critical);

            ordine o = { .numero = numero_ordini++, .persona = (char*)"Paolo" };

            spillone -> setOrdine(&o);

            int random_time = rand() % TEMPO_INSERIMENTO + 1;
            std::this_thread::sleep_for(std::chrono::seconds(random_time));

            std::cout << "Ordine " << numero_ordini << " e' stato inserito." << std::endl;

            queue_full.notify_one();

        critical.unlock();
    }
}


void prelevaOrdine(Spillone *spillone)
{
    srand(time(NULL));

    while (true)
    {
        critical.lock(); 
        
            if (spillone -> getNumeroOrdini() <= 0)
            {
                queue_full.wait(critical);
                critical.unlock();
                continue;
            }

            int random_time = rand() % TEMPO_INSERIMENTO + 1;
            std::this_thread::sleep_for(std::chrono::seconds(random_time));
   
            spillone -> rimuoviPrimoOrdine();
            std::cout << "Ordine " << numero_ordini-- << " e' stato prelevato." << std::endl;

            queue_empty.notify_one();

        critical.unlock();
    }
}


int main()
{
    Spillone spillone;

    std::thread cassiere(inserisciOrdine, &spillone);
    std::thread cuoco(prelevaOrdine, &spillone);

    cassiere.join();
    cuoco.join();

    return 0;
}


