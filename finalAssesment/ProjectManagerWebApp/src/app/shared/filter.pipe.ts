import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {
  // transform(items: any[], prop: string, searchText: string): any[] {
  //     if(!items) return [];
  //     if(!searchText) return items;
  //     searchText = searchText.toLowerCase();
  //     return items.filter( it => {
  //             return it[prop].toLowerCase().includes(searchText);
  //         });
  // }

  public transform(items: any, filter: any, isAnd: boolean): any {
    if (filter && Array.isArray(items)) {
      let filterKeys = Object.keys(filter);
      if (isAnd) {
        return items.filter(item =>
            filterKeys.reduce((memo, keyName) =>
                (memo && new RegExp(filter[keyName], 'gi').test(item[keyName])) || filter[keyName] === "", true));
      } else {
        return items.filter(item => {
          return filterKeys.some((keyName) => {
            //console.log(keyName);
            return new RegExp(filter[keyName], 'gi').test(item[keyName]) || filter[keyName] === "";
          });
        });
      }
    } else {
      return items;
    }
  }
}
