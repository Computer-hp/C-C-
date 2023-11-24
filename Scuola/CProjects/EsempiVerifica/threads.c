#include <stdio.h>
#include <unistd.h>
#include <pthread.h>

struct Argomenti
{
    char *name;
    int age;
};

typedef struct Argomenti Argomenti;

void *printThread(Argomenti *var1)
{
    printf("Name: %s  Age: %i", var1->name, var1->age);
}

int main()
{
    pthread_t thread1;// thread2;

    Argomenti var1;

    var1.name = "Giorgio";
    var1.age = 20;

    pthread_create(&thread1, NULL, (void *(*)(void *))printThread, &var1);
    pthread_join(thread1, NULL);

    return 0;
}