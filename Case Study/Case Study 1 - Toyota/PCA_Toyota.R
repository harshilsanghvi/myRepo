rm(list=ls())
car <- read.csv('C:/Users/HS/Desktop/BDAP/Case Study 13-8/ToyotaCorollaPCA.csv')
states <- row.names(car)
names(car)
# apply(car,2,mean)
# apply(car,2,var)
# ?apply
pr.out = prcomp(car,scale=TRUE)
names(pr.out)
pr.out$center
pr.out$scale
pr.out$rotation
dim(pr.out$x)
biplot(pr.out,scale=0)
pr.out$rotation=-pr.out$rotation
pr.out$x=-pr.out$x
biplot(pr.out,scale=0)
pr.out$sdev
pr.var=pr.out$sdev^2
pr.var
pve=pr.var/sum(pr.var)
pve
plot(pve,xlab="Principal Component", ylab = "Proportion of Variamce Explained",ylim=c(0,1),type="b")
plot(cumsum(pve),xlab="Principal Component",ylab="Cumulative Proportion",ylm=c(0,1),type="b")
LR <- read.csv('C:/Users/HS/Desktop/BDAP/Case Study 13-8/ToyotaCorollaPCA1.csv')


m1 = lm(Price ~ Age+KM+Weight,data=LR)
summary(m1)
library(car)
vif(m1)
