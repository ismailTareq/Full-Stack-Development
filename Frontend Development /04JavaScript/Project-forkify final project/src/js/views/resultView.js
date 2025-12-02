import View from "./View";
import icons from 'url:../../img/icons.svg';
import previewView from './previewView.js';
class ResultView extends View{
    _parentEL = document.querySelector('.results')
    _ErrorMessage = 'what the fuck was that there is no recipes known with this name dude!! '
    _message = ''
    _generateHtml(){
        return this._data.map(res => previewView.render(res,false)).join('')
    }
}

export default new ResultView()