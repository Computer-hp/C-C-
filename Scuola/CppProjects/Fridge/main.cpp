#include <iostream>
#include <cstring>
#include <sstream>

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
    int number_of_products;
  
  public:

    Fridge();

    Tag_RFID& get_product()     // using a reference so i don't return a copy of the Tag_RFID struct,
    {                           // and i can modify the value of (private) products outside the class
        return *products;
    }

    void set_new_product(Tag_RFID &value);

    void store_in_new_array(Tag_RFID* (&tmp), Tag_RFID* (&tmp2), int size);

    void print_products();

    string get_expiration_products(string current_date);

    int number_product_packaging(int id_cod);

    void save_products_in_file();
    void get_products_from_file();

    bool is_less(string& product_date, string& current_date);
};

Fridge::Fridge()
{
    products = new Tag_RFID[1]; 
    number_of_products = 1;

    products[0].id_cod = 69;
    products[0].description = "yogurt";
    products[0].expiration_date = "12:12:2023";
    products[0].calories = 1789;
}


void Fridge::set_new_product(Tag_RFID &value)
{
    number_of_products += 1;

    Tag_RFID *tmp = new Tag_RFID[number_of_products];

    store_in_new_array(tmp, products, number_of_products - 1);

    tmp[number_of_products - 1] = value;

    delete[] products;

    products = new Tag_RFID[number_of_products];

    store_in_new_array(products, tmp, number_of_products);

    delete[] tmp;
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
    int size = number_of_products;

    for (int i = 0; i < size; i++)
    {
        cout << "\nProduct n° " << i + 1<< '\n';
        cout << "\tId: " << products[i].id_cod << ' ' << "Description: " << products[i].description << ' ';
        cout << "Expiration date: " << products[i].expiration_date << ' ' << "Calories: " << products[i].calories << endl;
    }
}

bool Fridge::is_less(string& product_date, string& current_date)
{
    std::istringstream ss_product(product_date);
    std::istringstream ss_current(current_date);

    int day_product, month_product, year_product, day_current, month_current, year_current;
    char sep; // for the ':' separator

    ss_product >> day_product >> sep >> month_product >> sep >> year_product;
    ss_current >> day_current >> sep >> month_current >> sep >> year_current;

    if (year_product < year_current) {
        return true;
    } else if (year_product > year_current) {
        return false;
    } else if (month_product < month_current) {
        return true;
    } else if (month_product > month_current) {
        return false;
    } else {
        return day_product < day_current;
    }
}

string Fridge::get_expiration_products(string current_date)
{
    string str = "";
    
    int size = number_of_products;

    for (int i = 0; i < size; i++)
    {
        if (is_less(products[i].expiration_date, current_date))
        {
            str += products[i].expiration_date + " ";
        }
    }
    return str;
}

int Fridge::number_product_packaging(int id_cod)
{
    int counter = 0;
    for (int i = 0; i < number_of_products; i++)
    {
        if ((*products).id_cod == id_cod)
            counter++;
    }
    return counter;
}


/*int get_number_of_products(const char* file_path)
{
    FILE *f;

    f = fopen(file_path, "r");

    const char* product = "Product n°";

    int N = 0, size = strlen(product);

    char line[size];

    while (fgets(line, sizeof(line), f))
    {
        if (strcmp(line, product))
            N++;
    }

    fclose(f);

    return N;
}


void Fridge::save_products_in_file()
{

}

void Fridge::get_products_from_file()
{
    int N = get_number_of_products("file.txt"), properties_number = 4;

    FILE *f = fopen("file.txt", "r");

    char line[256];

    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < properties_number && fgets(line, sizeof(line), f) != NULL; ++i)
        {
            
        }
    }

    
    //(int)cod   (char*)description    (char*)expiration_date    (int)calories
    
    //products->eachproperty

    //get_each_property(int position, )




    fclose(f);
}*/



int main()
{
    Fridge f1;

    f1.print_products();

    Tag_RFID tag1
    {
        .id_cod = 123,
        .description = "sdfjadsfjs", 
        .expiration_date = "08:12:2023",
        .calories = 12343
    };

    f1.set_new_product(tag1);

    f1.print_products();

    cout << "\nExpired products: " << f1.get_expiration_products("01:11:2024");

    return 0;
}