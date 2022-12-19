#include "versionControl.h"

extern BOOL drawing;
extern TypeElement currentElement;
extern Element elem[elemBufferSize];
extern int currentIndex;
extern int elemCount;
extern Display display;
extern BOOL zooming;
extern POINT p1;

void initialize()
{
    CreateDirectoryA("./VersionControl", NULL);
    FILE *fptr = fopen("./VersionControl/BRANCHES.csv", "w");
    fprintf(fptr, "%s,%s,%s,%s\n", "branch_name", "full_path", "current", "last");
    char *commitName = rand_string_alloc(commitNameSize);
    fprintf(fptr, "%s,%s,%s,%s\n", rand_string_alloc(branchNameSize), commitName, commitName, commitName);
    fclose(fptr);
    // создать папку с начальным коммитом
    char full_path[9999] = "./VersionControl/";
    strcat(full_path, commitName);
    CreateDirectoryA(full_path, NULL);
    // начальный коммит
    char commit_path[9999] = "";
    strcat(commit_path, full_path);
    strcat(commit_path, "/");
    strcat(commit_path, commitName);
    strcat(commit_path, ".csv");
    FILE *fptr2 = fopen(commit_path, "w");
    // запись изменений (тут получается всё что есть записываем)
    fclose(fptr2);
}

void commit()
{
    FILE *branches = fopen("./VersionControl/BRANCHES.csv", "r");
    char buff2[9999];
    fgets(buff2, 9999, (FILE*)branches);
    int c;
    while ((c = getc(branches)) != EOF)
    {
        char buff[9999];
        buff[0]=c;
        fgets(&buff[1], 9998, (FILE*)branches);
        // сплит на столбцы
        char *branch_name = strtok(buff, ",");
        char *full_path = strtok(NULL, ",");
        char *current = strtok(NULL, ",");
        char *last = strtok(NULL, "\n");
        // если в current не пусто, то вытаскиваем путь до current включительно
        if (strcmp(current, "") != 0)
        {
            char *path = strtok(full_path, current);
            if (!path) path = "";
            // новая папка new внутри папки path/current
            
            if (strcmp(current, last) == 0)
            {
                // коммит на той же ветке
                // обновить full_path на path/current/new в BRANCHES.csv
                // обновить current и last на new в BRANCHES.csv

            }
            else
            {
                // новая ветка
                // новая строчка в BRANCHES.csv
                // branch_name сгенерировать в BRANCHES.csv
                // full_path = path/current/new в BRANCHES.csv
                // current и last = new в BRANCHES.csv
            }

            break;
        }
    }
    //char * commitName = rand_string_alloc(commitNameSize);
    //FILE *fptr = fopen(commitName, "mode");
}

char* rand_string_alloc(size_t size)
{
    char *s = malloc(size + 1);
    if (s) {
        rand_string(s, size);
    }
    return s;
}

char *rand_string(char *str, size_t size)
{
    const char charset[] = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    if (size)
    {
        --size;
        for (size_t n = 0; n < size; n++) {
            int key = rand() % (int) (sizeof charset - 1);
            str[n] = charset[key];
        }
        str[size] = '\0';
    }
    return str;
}