#include <iostream>
#include <string>

using namespace std;

typedef struct Tag_RFID
{
    int id_cod;
    string description, expiration_date;
    double calories;
} Tag_RFID;


class Fridge
{
  private:
    Tag_RFID *products;

  
  public:

    Fridge() { products = new Tag_RFID[1]; }

    Tag_RFID get_product()
    {
        return *products;
    }

    void set_new_product(Tag_RFID value);

    void store_in_new_array(Tag_RFID* (&tmp), Tag_RFID* (&tmp2), int size);

    void print_products();

    string get_expiration_products(string current_date);

    int number_product_packaging(int id_cod);

    void save_products_in_file();
    void get_products_from_file();

    bool is_less(string product_date, string current_date);

    static int get_array_length(Tag_RFID* &p)
    {
        return (sizeof(p) / sizeof(p[0]));
    }
};


void Fridge::set_new_product(Tag_RFID value)
    {
        int new_size = Fridge::get_array_length(products) + 1;

        Tag_RFID *tmp = new Tag_RFID[new_size];

        store_in_new_array(tmp, products, new_size);

        delete[] products;

        products = new Tag_RFID[new_size];

        store_in_new_array(products, tmp, new_size);

    }

void Fridge::store_in_new_array(Tag_RFID* (&tmp), Tag_RFID* (&tmp2), int size)
{
    for (int i = 0; i < size; i++)
    {
        tmp[i] = tmp2[i];
    }
}

void Fridge::print_products()
{
    int size = Fridge::get_array_length(this->products);

    cout << "\nProducts in the fridge:";

    for (int i = 0; i < size; i++)
    {
        cout << "\nProduct n° " << i << '\n';
        cout << products->id_cod << ' ' << products->description << ' ' << products->expiration_date << ' ' << products->calories;
    }
}

int get_year_month_(int v[], int size)
{
    
}

bool Fridge::is_less(string product_date, string current_date)
{
    int i = 0;

    int sum = 0, sum2 = 0;

    bool state = false;

    while (product_date[i] != '\0')
    {
        if (product_date[i] != ':')
        {
            sum += product_date[i];
            sum2 += current_date[i];
        }
        else if (sum < sum2)
        {
            state = true;

            sum = 0; sum2 = 0;
        }
        else
        {
            if (state)
                state = false;

            sum = 0; sum2 = 0;
        }
        i++;
    }

    return state;
}

string Fridge::get_expiration_products(string current_date)
{
    string str = "";
    
    int size = Fridge::get_array_length(this->products);

    for (int i = 0; i < size; i++)
    {
        if (is_less(products[i].expiration_date, current_date))
        {
            str += products[i].expiration_date + ", ";
        }
    }
}

int Fridge::number_product_packaging(int id_cod)
{

}

void Fridge::save_products_in_file()
{

}

void Fridge::get_products_from_file()
{

}







int main()
{



    return 0;
}