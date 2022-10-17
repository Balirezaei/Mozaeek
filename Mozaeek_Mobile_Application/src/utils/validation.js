
export const validation={
    
    emergencyPhone:{
        presence:{
            message:'لصفا تلفن ضروری خود را وارد کنید',
        }
    },
    address:{
        presence:{
            message:'لطفا آدرس را وارد کنید',
        }
    },
    name:{
        presence:{
            message:'نام خود را وارد کنید',
        }
    },
    rfp:{
        presence:{
            message:'لطفا تیتراژ مورد نظر خود را وارد کنید',
        },
        // formatNum:{
        //     message:'لطفا عدد وارد کنید',

        // }
    },
    dis:{
        presence:{
            message:'لطفا توضیحات مورد نظر خود را وارد کنید',
        }
    },

    date:{
        presence:{
            message:'لطفاد زمان پیشنهادی خود برای تحویل سفارش را وارد کنید',
        },
        // formatNum:{
        //     message:'لطفا عدد وارد کنید',

        // }
    },

    price:{
        presence:{
            message:'لطفا قیمت پیشنهادی خود را وارد کنید',
        },
        formatNum:{
            message:'لطفا عدد وارد کنید',

        }
    },

    notEmpty:{
        presence:{
            message:'این مورد را وارد کنید',
        },  
    },
    email:{
        presence:{
            message:'لطفا پست الکترونیکی خود را وارد کنید',

        },
        format:{
            pattern:/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
            message:'لطفا پست الکترونیکی معتبر وارد کنید',

        }
    },

    password:{
        presence:{
            message:'لطفا رمز عبور خود را وارد کنید',

        },
        length:{
            minimum:{
                val:6,
                message:'رمز عبور شما حداقل باید ۶ کاراکتر باشد',

            }

        }
    },
    phone:{
        presence:{
            message:'شماره تلفن خود را وارد کنید',

        },
        format:{
            pattern:/09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}/,
            // pattern:/(0|\+98)?([ ]|-|[()]){0,2}9[1|2|3|4|0]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}/,
            message: 'شماره تماس را درست وارد کنید'
        },
        length:{
            equal:{
                val:11,
                message:'شماره تلفن شما باید  ۱۱ عدد باشد',

            }

        }
    },

    text:{
        presence:{
            message:'این مورد را وارد کنید',

        },
        length:{
            minimum:{
                val:3,
                message:'طول متن باید حداقل ۳ کلمه باشد',

            }

        }
    },
    longText:{
        presence:{
            message:'این مورد را وارد کنید',

        },
        length:{
            minimum:{
                val:20,
                message:'طول متن باید حداقل ۲۰ کلمه باشد',

            }

        }
    },
    longerText:{
        presence:{
            message:'این مورد را وارد کنید',

        },
        length:{
            minimum:{
                val:40,
                message:'طول متن باید حداقل ۴۰ کلمه باشد',

            }

        }
    },
}


export function validate(nameField,value,lang='en') {

    let resp=[null,null]
    if(validation.hasOwnProperty(nameField)){
        let v=validation[nameField];
        if(value==='' || value===null){
            // console.warn('validation null');
            resp[0]=false;
            resp[1]=v['presence']['message'];

        }
        // else if(v.hasOwnProperty('formatNum') && typeof(value)!= Number){
        //     resp[0]=false;

        //         resp[1]=v['formatNum']['message'];

        // }
        else if(v.hasOwnProperty('format') &&  !v['format']['pattern'].test(String(value))){

               resp[0]=false;
               resp[1]=v['format']['message'];


        }else if(v.hasOwnProperty('length')){
            let l=v['length']
            if(l.hasOwnProperty('minimum') && value.length<l['minimum']['val']){
                resp[0]=false;

                    resp[1]=l['minimum']['message'];

            }else if(l.hasOwnProperty('maximum') && value.length>l['maximum']['val']){
                resp[0]=false;

                    resp[1]=l['maximum']['message'];

            }else if(l.hasOwnProperty('equal') && value.length!=l['equal']['val']){
                resp[0]=false;

                    resp[1]=l['equal']['message'];

            }else{
                resp[0]=true;
            }
        }else{
            resp[0]=true;
        }
    }else {
        resp[0]=true;
    }

   return resp;

}