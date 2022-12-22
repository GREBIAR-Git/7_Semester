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

    char branches_csv[9999] = "";
    str_concat(branches_csv, 2, vc_path, "/BRANCHES.csv");
    FILE *fptr = fopen(branches_csv, "wb");
    fprintf(fptr, "%s,%s,%s,%s\n", "branch_name", "full_path", "current", "last");
    char *commit_name = rand_string_alloc(commitNameSize);
    fprintf(fptr, "%s,%s,%s,%s\n", escape(rand_string_alloc(branchNameSize)), escape(commit_name), escape(commit_name), escape(commit_name));
    fclose(fptr);

    char full_path[9999] = "";
    str_concat(full_path, 3, vc_path, "/", commit_name);
    CreateDirectoryA(full_path, NULL);

    char commit_csv[9999] = "";
    str_concat(commit_csv, 4, full_path, "/", commit_name, ".csv");
    FILE *fptr2 = fopen(commit_csv, "wb");
    // содержимое файла с начальным коммитом
    writeInit(fptr2);
    fclose(fptr2);
    fptr2 = fopen(commit_csv, "rb");
    readChanges(fptr2);
    fclose(fptr2);
}

void commit()
{
    char branches_csv[9999] = "";
    str_concat(branches_csv, 2, vc_path, "/BRANCHES.csv");
    FILE *branches = fopen(branches_csv, "r+b");

    char buff_skip[9999];
    fgets(buff_skip, 9999, (FILE*)branches);

    int c;
    while ((c = getc(branches)) != EOF)
    {
        char buff[9999];
        buff[0]=c;
        long posBefore = ftell(branches);
        fgets(&buff[1], 9998, (FILE*)branches);
        long posAfter = ftell(branches);

        char *branch_name = strtok(buff, ",");
        char *full_path = strtok(NULL, ",");
        char *current = strtok(NULL, ",");
        char *last = strtok(NULL, "\n");

        if (!current) continue;

        char new_path[9999] = "";
        printf("|[%s]\n[%s]|", escape(full_path), escape(current));
        char *currentPos = strstr(full_path, current);
        memcpy(new_path, full_path, currentPos - full_path);
        new_path[currentPos - full_path] = '\0';
        char *commit_name = rand_string_alloc(commitNameSize);
        str_concat(new_path, 3, current, "/", commit_name);

        char dir_path[9999] = "";
        str_concat(dir_path, 3, vc_path, "/", new_path);
        CreateDirectoryA(dir_path, NULL);

        char commitFile[9999] = "";
        str_concat(commitFile, 4, dir_path, "/", commit_name, ".csv");
        FILE *newF = fopen(commitFile, "wb");
        writeDiff(newF);
        fclose(newF);

        if (strcmp(current, last) == 0)
        {
            fseek(branches, posBefore-posAfter - 1, SEEK_CUR);
            fprintf(branches, "%s,%s,%s,%s\n", escape(branch_name), escape(new_path), escape(commit_name), escape(commit_name));
        }
        else
        {
            fseek(branches, posBefore-posAfter - 1, SEEK_CUR);
            fprintf(branches, "%s,%s,,%s\n", escape(branch_name), escape(full_path), escape(last));
            fseek(branches, 0, SEEK_END);
            fprintf(branches, "%s,%s,%s,%s\n", escape(rand_string_alloc(branchNameSize)), escape(new_path), escape(commit_name), escape(commit_name));
        }
        break;
    }
    fclose(branches);
}

void nextCommit()
{
    char branches_csv[9999] = "";
    str_concat(branches_csv, 2, vc_path, "/BRANCHES.csv");
    FILE *branches = fopen(branches_csv, "r+b");

    char buff_skip[9999];
    fgets(buff_skip, 9999, (FILE*)branches);

    int c;
    while ((c = getc(branches)) != EOF)
    {
        char buff[9999];
        buff[0]=c;
        long posBefore = ftell(branches);
        fgets(&buff[1], 9998, (FILE*)branches);
        long posAfter = ftell(branches);

        char *branch_name = strtok(buff, ",");
        char *full_path = strtok(NULL, ",");
        char *current = strtok(NULL, ",");
        char *last = strtok(NULL, "\n");

        if (!current) continue;

        char *currentPos = strstr(full_path, current);
        char nextCurrent[999];
        memcpy(nextCurrent, currentPos + commitNameSize, commitNameSize-1);
        nextCurrent[commitNameSize-1] = '\0';

        fseek(branches, posBefore-posAfter - 1, SEEK_CUR);
        fprintf(branches, "%s,%s,%s,%s\n", escape(branch_name), escape(full_path), escape(nextCurrent), escape(last));
        break;
    }
    fclose(branches);
}

void prevCommit()
{
    char branches_csv[9999] = "";
    str_concat(branches_csv, 2, vc_path, "/BRANCHES.csv");
    FILE *branches = fopen(branches_csv, "r+b");

    char buff_skip[9999];
    fgets(buff_skip, 9999, (FILE*)branches);

    int c;
    while ((c = getc(branches)) != EOF)
    {
        char buff[9999];
        buff[0]=c;
        long posBefore = ftell(branches);
        fgets(&buff[1], 9998, (FILE*)branches);
        long posAfter = ftell(branches);

        char *branch_name = strtok(buff, ",");
        char *full_path = strtok(NULL, ",");
        char *current = strtok(NULL, ",");
        char *last = strtok(NULL, "\n");

        if (!current) continue;
        if (startsWith(full_path, current)) break;

        char *currentPos = strstr(full_path, current);
        char nextCurrent[999];
        memcpy(nextCurrent, currentPos - commitNameSize, commitNameSize-1);
        nextCurrent[commitNameSize-1] = '\0';

        fseek(branches, posBefore-posAfter - 1, SEEK_CUR);
        fprintf(branches, "%s,%s,%s,%s\n", escape(branch_name), escape(full_path), escape(nextCurrent), escape(last));
        break;
    }
    fclose(branches);
}

void writeInit(FILE *f)
{
    fprintf(f, "%s,%s,%s,%s,%s,%s,%s,%s\n", "#","x1", "y1", "x2", "y2", "shape", "size", "colour");
    for (int i = 0; i < elemCount - 1; i++)
    {
        fprintf(f, "%d,%lf,%lf,%lf,%lf,%d,%d,%lu\n", i, elem[i].coords.point1.x, elem[i].coords.point1.y, elem[i].coords.point2.x, elem[i].coords.point2.y, elem[i].properties.shape, elem[i].properties.size, elem[i].properties.colour);
    }
}

void calculateDiff(Element * result)
{
    // собираю новый elem на основе всех коммитов на ветке до current включительно
    Element last_saved_elem[elemBufferSize];
    

    // потом сравниваю новый собранный elem с основным - получаю разницу

    // собираю результирующий elem - result
}

void readChanges(FILE *f)
{
    char buff_skip[9999];
    fgets(buff_skip, 9999, (FILE*)f);

    int c;
    while ((c = getc(f)) != EOF)
    {
        char buff[9999];
        buff[0]=c;
        long posBefore = ftell(f);
        fgets(&buff[1], 9998, (FILE*)f);
        long posAfter = ftell(f);

        int id, shape, size;
        double x1, y1, x2, y2;
        COLORREF colour;

        sscanf(strtok(buff, ","), "%d", &id);
        sscanf(strtok(NULL, ","), "%lf", &x1);
        sscanf(strtok(NULL, ","), "%lf", &y1);
        sscanf(strtok(NULL, ","), "%lf", &x2);
        sscanf(strtok(NULL, ","), "%lf", &y2);
        sscanf(strtok(NULL, ","), "%d", &shape);
        sscanf(strtok(NULL, ","), "%d", &size);
        sscanf(strtok(NULL, "\n"), "%lu", &colour);

        printf("test:\n");
        printf("%d,%lf,%lf,%lf,%lf,%d,%d,%lu\n", id, x1, y1, x2, y2, shape, size, colour);
    }
}

void writeDiff(FILE *f)
{
    // 
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
    const char charset[] = "abcdefghijklmnopqrstuvwxyz0123456789";
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

BOOL startsWith(const char *str, const char *starts_with)
{
    size_t len = strlen(starts_with);
    return (strncmp(str, starts_with, len) == 0);
}

char* escape(char* buffer)
{
    int i,j;
    int l = strlen(buffer) + 1;
    char esc_char[]= { '\a','\b','\f','\n','\r','\t','\v','\\'};
    char essc_str[]= {  'a', 'b', 'f', 'n', 'r', 't', 'v','\\'};
  char* dest  =  (char*)calloc( l*2,sizeof(char));
    char* ptr=dest;
    for(i=0;i<l;i++){
        for(j=0; j< 8 ;j++){
            if( buffer[i]==esc_char[j] ){
              *ptr++ = '\\';
              *ptr++ = essc_str[j];
                 break;
            }
        }
        if(j == 8 )
      *ptr++ = buffer[i];
    }
  *ptr='\0';
    return dest;
}

void str_concat(char *result, int count, ...)
{
    va_list args;

    va_start(args, count);
    for(int i = 0; i < count; i++)
    {
        strcat(result, va_arg(args, char*));
    }
    va_end(args);
}

double toDouble(const char* s, int start, int stop)
{
    unsigned long long int m = 1;
    double ret = 0;
    for (int i = stop; i >= start; i--) {
        ret += (s[i] - '0') * m;
        m *= 10;
    }
    return ret;
}

void str_split(char* str, char* delimiter, int delimiter_len, int count, ...)
{
    va_list args;

    va_start(args, count);
    for(int i = 0; i < count; i++)
    {
        char* delimiterPos = strstr(str, delimiter);
        if (delimiterPos != NULL)
        {
            char *arg = va_arg(args, char*);
            memcpy(arg, str, delimiterPos - str);
            arg[delimiterPos - str] = '\0';
            str = delimiterPos + delimiter_len;
        }
        else
        {
            char *arg = va_arg(args, char*);
            memcpy(arg, str, delimiterPos - str);

        }
    }
    va_end(args);
}