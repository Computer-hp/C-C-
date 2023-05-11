def fattoriale(number):
    if number == 0:
        return 1
    else:
        return number * (fattoriale(number-1))

number = int(input("Inserisci un numero "))
print("Il fattoriale di ", number, "e'", fattoriale(number))