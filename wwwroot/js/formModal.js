//receber inputs
const Modal = {
    open() {
        //abrir modal

        document.querySelector(".modal-overlay").classList.add('active')

    },
    close (){
        console.log('hi')
        document.querySelector(".modal-overlay").classList.remove('active')

    }
}



const Form = {
    peso: document.querySelector('input#peso'),
    altura: document.querySelector('input#altura'),

    getValues() {
        peso= Form.peso.value
        altura = Form.altura.value },
    validateValues(){
        if(Form.peso.value<0||Form.altura.value<0){
            throw new Error("Imforme valores maiores que zero")
        }
    },
   

    submit(event) {
        //impede o envio do formulario
        event.preventDefault()
        try {
            
         Modal.open()
    
            
        } catch (e) {
            alert(e.message)
        }
       
    }
}
