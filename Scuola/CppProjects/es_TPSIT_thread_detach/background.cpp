#include <bits/stdc++.h>

/*
Scrivere un programma in C++ che gestisca i dati meteorologici della città di Vicenza. Il programma deve leggere e aggiornare periodicamente 
i dati di temperatura salvati su un file chiamato "meteo veneto.txt".
Il programma deve consistere nei seguenti componenti:

: Implementare una funzione background che esegua in background un loop infinito per aggiornare le temperature 
registrate nel file "meteo veneto.txt". La funzione deve leggere la temperatura attuale dal file, 
incrementarla di una unità e sovrascrivere il file con il nuovo valore ogni 20 secondi.

: Implementare una funzione printFromFile che legga la temperatura attuale dal file "meteo vicenza.txt" e la restituisca 
come valore intero. Se il file non può essere aperto, la funzione deve restituire -1.

: Implementare la funzione main che visualizzi un semplice menu all'utente con due opzioni:

    Opzione 1: Visualizzare la temperatura attuale di Vicenza utilizzando la funzione printFromFile.
    Opzione 0: Uscire dal programma.

Assicurarsi che il programma gestisca correttamente la concorrenza durante la lettura e 
la scrittura del file utilizzando le opportune tecniche di sincronizzazione.
*/

using namespace std;

string file_name = "meteo_veneto.txt";


bool file_is_opened = false;
bool file_is_being_updated = false;

mutex critical;
condition_variable_any file_is_occupied_by_background_thread, file_is_getting_read;

void background();
int generate_random_temperature();
int print_from_file();
void foo();

int main()
{
	srand(time(NULL));

	thread t(background);
	t.detach();

	thread t1(foo);
	t1.join();

	return 0;
}


void foo()
{
	while (true)
	{
		cout << "Temperatura Vicenza: " << print_from_file();
	}
}


int generate_random_temperature()
{
	return rand() % 45;
}


void background()
{
	while (true)
	{
		critical.lock();

		if (file_is_opened || file_is_being_updated)
			file_is_getting_read.wait(critical);

		file_is_opened = true;
		file_is_being_updated = true;

		ifstream in_file(file_name);
		ofstream out_file(file_name, std::ios::app);

		if (!in_file.is_open() || !out_file.is_open())
		{
			cout << "\nError occured while opening file " << file_name << '\n';
			critical.unlock();
			return;
		}

		string line, last_line;

		while (getline(in_file, line))
			last_line = line;

		if (last_line.empty())
		{
			out_file << "Temperatura Vicenza:" << '\n' << generate_random_temperature();
			critical.unlock();
			continue;
		}

		int new_temperature = stoi(last_line) + 1;
		this_thread::sleep_for(chrono::milliseconds(1500));
		
		out_file << "\nTemperatura Vicenza: " << '\n' << new_temperature;

		in_file.close();
		out_file.close();

		critical.unlock();

		file_is_opened = false;
		file_is_being_updated = false;
		file_is_occupied_by_background_thread.notify_one();
	}	
}


int print_from_file()
{
	critical.lock();

		if (file_is_opened || file_is_being_updated)
			file_is_occupied_by_background_thread.wait(critical);

		file_is_opened = true;

		ifstream in_file(file_name);

		if (!in_file.is_open()) 
		{
			critical.unlock();
			return -1;
		}

		string line, last_line;

		while (getline(in_file, line))
			last_line = line;

		in_file.close();

	critical.unlock();

	file_is_opened = false;
	file_is_getting_read.notify_one();

	return stoi(last_line);
}