import * as moment from 'moment';

export function renderDate(date: Date) {
  return moment(date).format("YYYY/MM/DD");
}

export function renderYesNo(value: boolean, locale?: any) {
  return (value ? "Sí" : "No");
}
