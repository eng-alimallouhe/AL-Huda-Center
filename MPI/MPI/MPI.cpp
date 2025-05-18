#include <iostream>
#include <mpi.h>

using namespace std;

int main()
{
    MPI_Init(NULL, NULL);
    cout << "Hello World!\n";
    MPI_Finalize();
    return 0;
}