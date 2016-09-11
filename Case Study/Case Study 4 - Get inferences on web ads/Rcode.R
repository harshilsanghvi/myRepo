rm(list=ls())
products <- read.csv('C:/Users/HS/Desktop/BDAP/Case Study 13-8/CS2/CaseStudy8.csv')
products
install.packages("ggplot2")
install.packages("scales")
library(scales)
library(ggplot2)
d <- diamonds[sample(nrow(diamonds), 1000), ]
# note how size is automatically scaled and added as hover text
plot_ly(d, x = carat, y = price, size = carat, mode = "markers")

crime <- read.csv('C:/Users/HS/Desktop/BDAP/Case Study 13-8/CS2/CaseStudy8.csv',header=TRUE)
crime
p <- ggplot(crime, aes(Cost,Revenue,size=population))
p <- geom_point()
p <- p+geom_point(colour="red")+scale_size_area(4)+geom_text(size=3)
p + xlab("Murders per 1,000 population") + ylab("Burglaries per 1,000")
