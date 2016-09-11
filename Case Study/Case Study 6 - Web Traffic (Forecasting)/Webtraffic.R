
install.packages("tidyr")
library(tidyr)
rm(list = ls())

#Krunal Code
WT <- read.csv("C:/Users/HS/Desktop/BDAP/Case Study/Case Study 6 - Web Traffic (Forecasting)/Webtraffic.csv",header = T)
colnames(WT)[1] = "Year"
WT <- gather(WT,Year,"Traffic count",factor_key = TRUE)
colnames(WT)[2] = "Months"
WT <- WT[order(WT$Year, WT$Months),]


for (j in names(web))
{
  if(j!="Year")
    {
    for (i in web[j]){
      vec <- c(vec,i)
    }
  }
}
vec
ve <- seq(0,143,1)
n <- data.frame(ve,vec)
names(df)
df[1]
