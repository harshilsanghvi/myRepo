
# coding: utf-8

# In[ ]:

import os, sys, string, random
from os import listdir
def programs(*args):
    alpha = string.ascii_lowercase # get lowercase alphabets
    try:
        paths = args[1]
    except IndexError:
        paths = "D:/Setup/"
    for filename in listdir(paths):
        for i in range(0,len(alpha)):
            if filename.startswith(alpha[i]) and filename.endswith('.txt'):
                g = random.randint(0,25)
                newfilename = alpha[g]+filename[1:]
                print(filename ,'> file changed to >',newfilename)
                os.rename(paths+filename,paths+newfilename)
programs(sys.argv[1])


# In[ ]:



