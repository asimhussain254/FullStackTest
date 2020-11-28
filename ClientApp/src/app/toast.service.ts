import { Injectable, TemplateRef } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ToastService {
  toasts: any[] = [];

  success(message: string) {
    this.show(message, { classname: 'bg-success text-light' });
  }

  error(message: string) {
    this.show(message, { classname: 'bg-danger text-light' });
  }

  private show(textOrTpl: string | TemplateRef<any>, options: any = {}) {
    this.toasts.push({ textOrTpl, ...options });
  }

  remove(toast) {
    this.toasts = this.toasts.filter((t) => t !== toast);
  }
}
