# OzonTestMailSender
Script for create table in <img src="https://user-images.githubusercontent.com/56364684/192102219-bc99b504-0670-4598-86fd-c5041d66f239.png" alt="drawing" width="50"/>
```
create table main."EmailMessage"
(
    "Id"                   SERIAL PRIMARY KEY,
    "Recipient"            text not null,
    "Subject"              text not null,
    "Text"                 text not null,
    "CarbonCopyRecipients" text[],
    "Status"               integer	
);
```
