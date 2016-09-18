install.packages("lpSolve")
library(lpSolve)
lp(ddirection = "min",c(1000000,500000,120000,50000),)

f.con <- matrix (c(1, 2, 3, 4, 5, 3, 4 ,8), nrow=2, byrow=TRUE)
f.dir <- c(">=", ">=",">=",">=")
f.rhs <- c(5, 3, 4 ,8 )

lp (direction = "min", objective.in, const.mat, const.dir, const.rhs,
    transpose.constraints = TRUE, int.vec, presolve=0, compute.sens=0,
    binary.vec, all.int=FALSE, all.bin=FALSE, scale = 196, dense.const,
    num.bin.solns=1, use.rw=FALSE)


