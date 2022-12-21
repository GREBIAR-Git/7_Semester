#include "versionControl.h"

extern BOOL drawing;
extern ElementProperties currentElement;
extern Element elem[elemBufferSize];
extern int currentIndex;
extern int elemCount;
extern Display display;
extern BOOL zooming;
extern POINT p1;

const char* vc_path = "./VersionControl";

void initialize()
{
    CreateDirectoryA(vc_path, NULL);
    char vc_branches[9999] = "";
    strcat(vc_branches, vc_path);
    strcat(vc_branches, "/BRANCHES.csv");
    FILE *fptr = fopen(vc_branches, "w");
    fprintf(fptr, "%s,%s,%s,%s\n", "branch_name", "full_path", "current", "last");
    char *commitName = rand_string_alloc(commitNameSize);
    fprintf(fptr, "%s,%s,%s,%s\n", rand_string_alloc(branchNameSize), commitName, commitName, commitName);
    fclose(fptr);
    // папка системы контроля версий
    char full_path[9999] = "";
    strcat(full_path, vc_path);
    strcat(full_path, "/");
    // папка с начальным коммитом
    strcat(full_path, commitName);
    CreateDirectoryA(full_path, NULL);
    // файл с начальным коммитом
    char commit_path[9999] = "";
    strcat(commit_path, full_path);
    strcat(commit_path, "/");
    strcat(commit_path, commitName);
    strcat(commit_path, ".csv");
    FILE *fptr2 = fopen(commit_path, "w");
    // содержимое файла с начальным коммитом
    writeInit(fptr2);
    fclose(fptr2);
}

void commit()
{
    char vc_branches[9999] = "";
    strcat(vc_branches, vc_path);
    strcat(vc_branches, "/BRANCHES.csv");
    FILE *branches = fopen(vc_branches, "r+");
    char buff2[9999];
    fgets(buff2, 9999, (FILE*)branches);
    int c;
    while ((c = getc(branches)) != EOF)
    {
        char buff[9999];
        buff[0]=c;
        long posBefore = ftell(branches);
        fgets(&buff[1], 9998, (FILE*)branches);
        long posAfter = ftell(branches);
        // сплит на столбцы
        char *branch_name = strtok(buff, ",");
        char *full_path = strtok(NULL, ",");
        char *current = strtok(NULL, ",");
        char *last = strtok(NULL, "\n");
        if (!current) continue;
        // вытаскиваем путь до current включительно
        char *path = strtok(full_path, current);
        // новая папка внутри папки path/current
        char new_path[9999] = "";
        if (path) strcat(new_path, path);
        strcat(new_path, current);
        strcat(new_path, "/");
        char *commitName = rand_string_alloc(commitNameSize);
        strcat(new_path, commitName); // ("path" ? "path" : "") + "current/commitName"
        char dirPath[9999] = "";
        // можно вынести в отдельную функцию - получение vc_path со / и который является char asd[9999]
        strcat(dirPath, vc_path); // можно вынести 1
        strcat(dirPath, "/"); // можно вынести 2
        strcat(dirPath, new_path); // "./VersionControl/new_path" // можно вынести 3
        CreateDirectoryA(dirPath, NULL);
        char commitFile[9999] = "";
        strcat(commitFile, dirPath);
        strcat(commitFile, "/");
        strcat(commitFile, commitName);
        strcat(commitFile, ".csv");
        FILE *newF = fopen(commitFile, "w");
        writeDiff(newF);
        fclose(newF);
        // обновляем BRANCHES.csv
        if (strcmp(current, last) == 0)
        {
            fseek(branches, posBefore-posAfter, SEEK_CUR);
            fprintf(branches, "%s,%s,%s,%s\n", branch_name, new_path, commitName, commitName);
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
    fclose(branches);
}

void nextCommit()
{

}

void prevCommit()
{
    
}

void writeInit(FILE *f)
{
    fprintf(f, "%s,%s,%s,%s,%s,%s,%s,%s\n", "#","x1", "y1", "x2", "y2", "shape", "size", "colour");
    for (int i = 0; i < elemCount - 1; i++)
    {
        fprintf(f, "%d,%lf,%lf,%lf,%lf,%d,%d,%lu\n", i, elem[i].coords.point1.x, elem[i].coords.point1.y, elem[i].coords.point2.x, elem[i].coords.point2.y, elem[i].properties.shape, elem[i].properties.size, elem[i].properties.colour);
    }
}

void writeDiff(FILE *f)
{
    fprintf(f, "%s,%s,%s,%s,%s,%s,%s,%s\n", "#","x1", "y1", "x2", "y2", "shape", "size", "colour");
    for (int i = 0; i < elemCount - 1; i++)
    {
        fprintf(f, "%d,%lf,%lf,%lf,%lf,%d,%d,%lu\n", i, elem[i].coords.point1.x, elem[i].coords.point1.y, elem[i].coords.point2.x, elem[i].coords.point2.y, elem[i].properties.shape, elem[i].properties.size, elem[i].properties.colour);
    }
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