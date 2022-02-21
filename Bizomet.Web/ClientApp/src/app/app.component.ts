import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(private primengConfig: PrimeNGConfig) { }

  layout = 'purple';
  theme = 'purple';
  menuMode = 'horizontal';
  colorScheme = "dark";

  ngOnInit() {
    this.primengConfig.ripple = true;
    let scheme = localStorage.getItem('color-scheme') || this.colorScheme;
    if (this.colorScheme != scheme) {
      this.changeColorScheme(scheme);
    }
  }

  changeColorScheme(scheme) {
    this.changeStyleSheetsColor('layout-css', 'layout-' + scheme + '.css', 1);
    this.changeStyleSheetsColor('theme-css', 'theme-' + scheme + '.css', 1);
    this.colorScheme = scheme
    localStorage.setItem('color-scheme', scheme);
  }

  changeStyleSheetsColor(id, value, from) {
    const element = document.getElementById(id);
    const urlTokens = element.getAttribute('href').split('/');

    if (from === 1) {           // which function invoked this function - change scheme
      urlTokens[urlTokens.length - 1] = value;
    } else if (from === 2) {       // which function invoked this function - change color
      urlTokens[urlTokens.length - 2] = value;
    }

    const newURL = urlTokens.join('/');

    this.replaceLink(element, newURL);
  }

  changeTheme(theme) {
    const themeLink: HTMLLinkElement = document.getElementById('theme-css') as HTMLLinkElement;
    const href = 'assets/theme/' + theme + '/theme-' + this.colorScheme + '.css';
    this.theme = theme;
    this.replaceLink(themeLink, href);
  }

  changeLayout(layout) {
    const layoutLink: HTMLLinkElement = document.getElementById('layout-css') as HTMLLinkElement;
    const href = '../../assets/layout/css/' + layout + '/layout-' + this.colorScheme + '.css';
    this.layout = layout;
    this.replaceLink(layoutLink, href);
  }

  isIE() {
    return /(MSIE|Trident\/|Edge\/)/i.test(window.navigator.userAgent);
  }

  replaceLink(linkElement, href) {
    if (this.isIE()) {
      linkElement.setAttribute('href', href);
    } else {
      const id = linkElement.getAttribute('id');
      const cloneLinkElement = linkElement.cloneNode(true);

      cloneLinkElement.setAttribute('href', href);
      cloneLinkElement.setAttribute('id', id + '-clone');

      linkElement.parentNode.insertBefore(cloneLinkElement, linkElement.nextSibling);

      cloneLinkElement.addEventListener('load', () => {
        linkElement.remove();
        cloneLinkElement.setAttribute('id', id);
      });
    }
  }
}