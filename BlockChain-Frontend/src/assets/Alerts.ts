import swal from "sweetalert2";
export class Alerts{
    public showMiningSpinner(){
        swal.fire({
            title: "Minando",
            html: "Espere por favor.",
            allowOutsideClick: false,
            allowEscapeKey: false,
            showConfirmButton: false,
            imageUrl: 'src/assets/mining.gif',
            imageHeight: 200,
      
            didOpen: () => {
              // swal.showLoading()
            }
          })
    }

    public confirm(titleD: string, titleS: string, textS: string, iconS: any): boolean{
        swal.fire({
            title: titleD,
            showCancelButton: true,
            confirmButtonText: 'Yes'
        }).then(function(result) { 
            if (result.value) {
                swal.fire({
                    title: titleS,
                    text: textS,
                    icon: iconS,
                    showConfirmButton: false,
                    timer: 2000
                })            
                return true;
            }
            });
            return false;
    }


    public message(titleD:string, textD:string, iconD:any, timerD:number){
        swal.fire({
            title: titleD,
            text: textD,
            icon: iconD,
            showConfirmButton: false,
            timer: timerD
        })
    }
}

