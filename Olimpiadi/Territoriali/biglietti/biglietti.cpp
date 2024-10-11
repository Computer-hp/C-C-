#include <stdio.h>
#include <assert.h>


int compra(int N, int M, int A, int B) 
{
	int totale_centesimi_corse = N * A;


	if (N <= M) 
		
		if (totale_centesimi_corse < B)
			return totale_centesimi_corse;

		else
			return B;

	int volte = N / M;

	int totale_carnet = volte * B;
	int resto = N % M;

	if (resto == 0)
		
		if (totale_centesimi_corse < totale_carnet)
			return totale_centesimi_corse;
		else
			return totale_carnet;



	int totale_carnet_e_corse = volte * B + resto * A;
	int max_carnet = (volte + 1) * B;

	if (totale_centesimi_corse < totale_carnet_e_corse)
		if (totale_centesimi_corse < max_carnet)
			return totale_centesimi_corse;
		else
			return max_carnet;

	if (totale_carnet_e_corse < max_carnet)
		return totale_carnet_e_corse;
	else
		return max_carnet;
	

	return -1;

}


int main() 
{
	FILE *fr, *fw;
    	int N, M, A, B;

    	fr = fopen("input.txt", "r");
    	fw = fopen("output.txt", "w");

    	assert(4 == fscanf(fr, "%d%d%d%d", &N, &M, &A, &B));

    	fprintf(fw, "%d\n", compra(N, M, A, B));
    	fclose(fr);
    	fclose(fw);

    	return 0;
}
