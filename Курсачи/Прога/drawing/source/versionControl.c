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
    RemoveDirectoryA("./VersionControl");
    CreateDirectoryA("./VersionControl", NULL);
    FILE *fptr = fopen("./VersionControl/BRANCHES.csv", "w");
    fprintf(fptr, "%s,%s,%s,%s\n", "branch_name", "full_path", "current", "last");
    char * commitName = rand_string_alloc(commitNameSize);
    fprintf(fptr, "%s,%s,%s,%s\n", rand_string_alloc(branchNameSize), commitName, commitName, commitName);
    fclose(fptr);
    // создать папку с начальным коммитом
    char *full_path = "./VersionControl/";
    charConcat(&full_path, &commitName);
    CreateDirectoryA(full_path, NULL);
    // начальный коммит
    char *commit_path;
    charCopy(&commit_path, &full_path);
    charConcat1(&commit_path, '/');
    charConcat(&commit_path, &commitName);
    char *ext = ".csv";
    charConcat(&commit_path, &ext);
    puts(commit_path);
    // запись изменений (тут получается всё что есть записываем)
    //FILE *fptr = fopen(commit_path, "w");
}

void commit()
{
    FILE *branches = fopen("./VersionControl/BRANCHES.csv", "r");
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
        if (strcpy(current, "" != 0))
        {
            char *path = strtok(full_path, current);
            // новая папка new внутри папки path/current
            puts(path);

            if (strcpy(current, last) == 0)
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