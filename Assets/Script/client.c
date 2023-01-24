#include <sys/stat.h>
#include <sys/types.h>
#include <netdb.h>
#include <stdlib.h>
#include <unistd.h>


void creeSocket(char* ip,int port,int fd_socket)
{
    struct sockaddr_in socket_struct;

    socket_struct.sin_addr = ip;
    socket_struct.sin_family = AF_INET;
    socket_struct.sin_port = htons(port);
}

int main(int argc,char* argv[])
{
    char* ip = argv[1];
    int port = atoi(argv[2]);

    return 0;
}