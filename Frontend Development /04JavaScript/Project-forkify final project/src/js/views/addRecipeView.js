import View from "./View";
import icons from 'url:../../img/icons.svg';

class AddReciepeView extends View{
    _parentEL = document.querySelector('.upload')
    _window = document.querySelector('.add-recipe-window')
    _overlay = document.querySelector('.overlay')
    _btnOpen = document.querySelector('.nav__btn--add-recipe')
    _btnClose = document.querySelector('.btn--close-modal')
    _message = 'Recipe was successfully uploaded god forbid :]';
    
    constructor(){
        super();
        this._addHandlerShowWindow();
        this._addHandlerCloseWindow();
    }
    
    toggle()
    {
        this._overlay.classList.toggle('hidden')
        this._window.classList.toggle('hidden')
    }

    _addHandlerShowWindow(){
        this._btnOpen.addEventListener('click',this.toggle.bind(this))
    }

    _addHandlerCloseWindow(){
        this._btnClose.addEventListener('click',this.toggle.bind(this))
        this._overlay.addEventListener('click',this.toggle.bind(this))
    }

    addHandlerUpload(handle){
        this._parentEL.addEventListener('submit',function(e){
            e.preventDefault()
            const dataarr = [...new FormData(this)]
            const data = Object.fromEntries(dataarr)
            handle(data)
        })
    }

    _generateHtml() {
        
    }
}


export default new AddReciepeView()