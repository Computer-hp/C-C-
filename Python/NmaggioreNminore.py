l = int(input("Inserisci la lunghezza dell'array: "))
arr = []

for i in range(l):
    element = int(input("Inserisci un numero ".format(i)))
    arr.append(element)

max = arr[0]

for i in range(len(arr)):
    tempMax = 0
    for j in range(len(arr)):
        if (arr[i] > arr[j]):
            tempMax = arr[i]
        else:
            tempMax = arr[j]

        if (tempMax > max):
            max = tempMax

print("Il numero più grande e'", max)