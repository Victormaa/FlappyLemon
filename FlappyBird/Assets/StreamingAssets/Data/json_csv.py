#!/usr/bin/env python3.85
#Need to check if the files are empty entirely

import json
import fileinput
import os
import sys
import copy
import csv
import os.path
from os import path

#arguments are: 1: the directory
def main():
    directory = sys.argv[1];
      
    outputfile = directory + "_output.csv";

    filelist = os.listdir(directory);
    
    print(directory + "/" + filelist[0]);

    headers = [];
    rows = [];
    columns = {};
    for file in filelist:
        if file.endswith(".json"):   
            file = open(directory + "/" + filelist[0], 'r');
            print(file);
            j_data = json.load(file);

            file.close();

            for j in j_data:
                if len(headers) != 0:
                    for h in headers:
                        if h not in headers:
                            headers.append(h);
                else:
                    for h in j.keys():
                        headers.append(str(h));
            
            print(headers);


            for j in j_data:
                row = [];
                for h in headers:
                    row.append(j[h]);
                    #print(j[h]);
                rows.append(row);

            for h in headers:
                column = [];
                for j in j_data:
                    column.append(j[h]);
                columns[h] = column
  
    if(path.exists(outputfile)):
        os.remove(outputfile);

    if(path.exists('_' + outputfile)):
        os.remove('_' + outputfile);

    with open(outputfile, mode="w", newline='') as outfile:
        write = csv.writer(outfile, delimiter = ',')
        write.writerow(headers);
        for row in rows:
            print(row)
            if len(row) != 0:
                write.writerow(row);

    #with open(outputfile) as in_file:
    #    with open('_' + outputfile, 'w') as out_file:
    #        writer = csv.writer(out_file)
    #        for row in csv.reader(in_file):
    #            if any(field.strip() for field in row):
    #                writer.writerow(row)
    #
    #if(path.exists(outputfile)):
    #    os.remove(outputfile);
    
if __name__ == "__main__":
    main()