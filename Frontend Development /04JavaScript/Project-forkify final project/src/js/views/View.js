import icons from 'url:../../img/icons.svg';
export default class View{
    _data

    renderSpinner = function()
    {
        const Html = `
            <div class="spinner">
                <svg>
                <use href="${icons}.svg#icon-loader"></use>
                </svg>
            </div>
  `         ;
        this._parentEL.innerHTML = '';
        this._parentEL.insertAdjacentHTML('afterbegin', Html);
    }
    render(data,render = true)
    {
        if(!data || (Array.isArray(data) && data.length === 0))return this.renderError()
        this._data = data
        const Html = this._generateHtml()
        if (!render)return Html
        this._clear()
        this._parentEL.insertAdjacentHTML('afterbegin', Html);
    }

    update(data){
      // if(!data || (Array.isArray(data) && data.length === 0))return this.renderError()
      this._data = data
      const newHtml = this._generateHtml()
      const DOM = document.createRange().createContextualFragment(newHtml)
      const virtualElement = Array.from(DOM.querySelectorAll('*'))
      const realElement = Array.from(this._parentEL.querySelectorAll('*'))
      virtualElement.forEach((newEl,i) =>{
        const curEl = realElement[i]
        if(!newEl.isEqualNode(curEl) && newEl.firstChild?.nodeValue?.trim() !== ''){
          curEl.textContent = newEl.textContent
        }
        if(!newEl.isEqualNode(curEl)){
          Array.from(newEl.attributes).forEach(attr => {
            curEl.setAttribute(attr.name,attr.value)
          })
        }
      })
    }

    _clear(){
        this._parentEL.innerHTML = '';
    }

    renderError(msg = this._ErrorMessage){
        const Html = `
            <div class="error">
            <div>
              <svg>
                <use href="${icons}.svg#icon-alert-triangle"></use>
              </svg>
            </div>
            <p>${msg}</p>
          </div>
        `
        this._clear()
        this._parentEL.insertAdjacentHTML('afterbegin', Html);
    }
    renderSuccMessages(msg = this._message){
        const Html = `
            <div class="message">
            <div>
              <svg>
                <use href="${icons}.svg#icon-smile"></use>
              </svg>
            </div>
            <p>${msg}</p>
          </div>
        `
        this._clear()
        this._parentEL.insertAdjacentHTML('afterbegin', Html);
    }
}