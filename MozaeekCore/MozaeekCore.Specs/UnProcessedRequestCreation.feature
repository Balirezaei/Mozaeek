Feature: UnProcessedRequestCreation
	In order to create a new UnProcessedRequest
	As a WorkWriter
	I want to create new UnProcessedRequest with corresponding information

@mytag
Scenario: Create a new UnProcessedRequest
	Given 'Ali' is a WorkWriter in system
	And he has entered the following information 
	| Title											| Summery |
	| 'مدارک لازم جهت دریافت کارت بازرگانی'		| '1-داشتن حداقل23 سال تمام 2 –داشتن 3 سال سابقه فعالیت تجاری یا تولیدی به تأییددو نفر از دارندگان کارت بازرگانی با ارائه مدارک دانشگاهی یا تائیداتاق بازرگانی یا اتاق تعاون 3-عدم اشتغال تمام وقت و نداشتن رابطه استخدامی با قوای سه گانه 4 –تکمیل تقاضانامه 5 -کارتپایان خدمت یا معافیت 6ـ اصل و تصویر کارت ملی و شناسنامه 7 -نداشتن سوءپیشینه 8-حضور فرد متقاضی در اداره الزامی است 9 -پرداخت مبلغ 000/70 ریال به حساب بانکی اتاق '       |
	When he submits the data 
	Then the result should be an UnProcessedRequest with isProcessed False value