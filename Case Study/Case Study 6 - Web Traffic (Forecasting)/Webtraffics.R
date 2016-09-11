install.packages("timeSeries")
library(timeSeries)
webtraffic <-read.csv("C:/Users/HS/Desktop/BDAP/Case Study/Case Study 6 - Web Traffic (Forecasting)/Webtraffic.csv")
webtraffic

#Krunal Code
WT <- read.csv("C:/Users/HS/Desktop/BDAP/Case Study/Case Study 6 - Web Traffic (Forecasting)/Webtraffic.csv",header = T)
colnames(WT)[1] = "Year"
WT <- gather(WT,Year,"Traffic count",factor_key = TRUE)
colnames(WT)[2] = "Months"
WT <- WT[order(WT$Year, WT$Months),]
names(WT)

wt <- ts(WT$`Traffic count`, start=c(1949,1), frequency=12)
wt
startYear <- 1949
endYear <- 1960
# to forecast webtraffic in the next four years
nYearAhead <- 2

fit <- arima(wt, order=c(1,0,0), list(order=c(2,1,0), period=12))
fore <- predict(fit, n.ahead=12*nYearAhead)

# error bounds at 95% confidence level
U <- fore$pred + 2*fore$se
L <- fore$pred - 2*fore$se
ts.plot(wt, fore$pred, U, L, col=c(1,2,4,4), lty = c(1,1,2,2))
legend("topleft", c("Actual", "Forecast", "Error Bounds (95% Confidence)"),
       col=c(1,2,4), lty=c(1,1,2))

ts.plot(fore$pred, U, L, col=c(2,4,4), lty = c(1,2,2))
legend("topleft", c("Forecast", "Upper Bound (95% Confidence)", 
                    "Lower Bound (95% Confidence)"), col=c(2,4,4), lty=c(1,2,2))
