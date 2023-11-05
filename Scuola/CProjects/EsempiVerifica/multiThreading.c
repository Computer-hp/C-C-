#include <stdio.h>
#include <unistd.h>
#include <pthread.h>

struct Animal
{
    char *name;
    int numberOfLegs;
};

typedef struct Animal Animal;

void *print(Animal *animal){
    printf("\nAnimal name: %s\nNumber of legs: %i\n", animal->name, animal->numberOfLegs);
}


int main()
{
    pthread_t thread1, thread2;

    Animal cat, dog;

    cat.name = "Cat";
    cat.numberOfLegs = 4;

    dog.name = "Dog";
    dog.numberOfLegs = 4;


    pthread_create(&thread1, NULL, (void * (*)(void *))print, &cat);
    pthread_create(&thread2, NULL, (void * (*)(void *))print, &dog);

    pthread_join(thread1, NULL);
    pthread_join(thread2, NULL);


    return 0;
}