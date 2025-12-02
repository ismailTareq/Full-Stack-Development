import icons from 'url:../../img/icons.svg';
import View from './View.js'
import previewView from './previewView.js';
// import Fraction from 'fractional';
// console.log(Fraction)
class BookMarkView extends View{
    _parentEL = document.querySelector('.bookmarks__list');
    _ErrorMessage = 'no Bookmarks yet!'
    _message = ''
    
    addHandlerRender(handle){
        window.addEventListener('load',handle)
    }

    _generateHtml(){
        // console.log(this._data)
        return this._data.map(res => previewView.render(res,false)).join('')
    }

}

export default new BookMarkView()