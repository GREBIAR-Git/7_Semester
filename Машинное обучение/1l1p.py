def prt(a, d):
    print("№"+ str(a) + " - "+ str(d))

import numpy as np
scores = np.array([[20, 40, 56, 80, 0, 5, 25, 27, 74, 1], #[0, 1, 5, 20, 25, 27, 40, 56, 74, 80]
         [0, 98, 67, 100, 8, 56, 34, 82, 100, 7],
         [78, 54, 23, 79, 100, 0, 0, 42, 95, 83],
         [51, 50, 47, 23, 100, 94, 25, 48, 38, 77],
         [90, 87, 41, 89, 52, 0, 5, 17, 28, 99],
         [32, 18, 21, 18, 29, 31, 48, 62, 76, 22],
         [6, 0, 65, 78, 43, 22, 38, 88, 94, 100],
         [77, 28, 39, 41, 0, 81, 45, 54, 98, 12],
         [66, 0, 88, 0, 44, 0, 55, 100, 12, 11],
         [17, 70, 86, 96, 56, 23, 32, 49, 70, 80],
         [20, 24, 76, 50, 29, 40, 3, 2, 5, 11],
         [33, 63, 28, 40, 51, 100, 98, 87, 22, 30],
         [16, 54, 78, 12, 25, 35, 10, 19, 67, 0],
         [100, 88, 24, 33, 47, 56, 62, 34, 77, 53],
         [50, 89, 70, 72, 56, 29, 15, 20, 0, 0]])

prt(1,np.sum(scores==0))

prt(2,np.sum(scores>50))

prt(3,np.sum(scores>50) - np.sum(scores>=70))

prt(4,np.average(scores, axis=1).argmax())

nonzero = scores[np.where(scores>0)]
prt(5,nonzero)

prt(6,nonzero.min())

advanced = scores[np.where(scores>80)]
prt(7,advanced)

prt(8,np.median(scores, axis=1))

prt(9,np.average(scores.reshape(10,15),axis=1))

prt(10,advanced.size)

sto = np.where(scores==100,True,False)

prt(11,sto)

prt(12,scores[0:7])

