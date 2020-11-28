import { Injectable } from '@angular/core';
import { NgbDateAdapter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

@Injectable({
  providedIn: 'root',
})
export class CustomNgbDateNativeUTCAdapter extends NgbDateAdapter<Date> {
  fromModel(date: Date | string): NgbDateStruct {
    let result: NgbDateStruct;
    if (date instanceof Date) {
      result = {
        year: date.getFullYear(),
        month: date.getMonth() + 1,
        day: date.getDate(),
      };
    }

    if (typeof date === 'string') {
      const parsedDate = new Date(date);
      result = {
        year: parsedDate.getFullYear(),
        month: parsedDate.getMonth() + 1,
        day: parsedDate.getDay(),
      };
    }

    return result;
  }

  toModel(date: NgbDateStruct): Date {
    return date && date.year && date.month
      ? new Date(Date.UTC(date.year, date.month - 1, date.day))
      : null;
  }
}
