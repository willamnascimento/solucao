import { MatPaginatorIntl } from "@angular/material/paginator";

export function CustomPaginator(): MatPaginatorIntl {
    const customPaginatorIntl = new MatPaginatorIntl();
    
    customPaginatorIntl.itemsPerPageLabel = "Itens por página";
    customPaginatorIntl.lastPageLabel = "Última página";
    customPaginatorIntl.firstPageLabel = "Primeira página";
    customPaginatorIntl.nextPageLabel = "Próxima página";
    customPaginatorIntl.previousPageLabel = "Página anterior";
    customPaginatorIntl.getRangeLabel = (page: number, pageSize: number, length: number) => {
        if (length === 0 || pageSize === 0) {
          return `0 de ${length }`;
        }
        length = Math.max(length, 0);
        const startIndex = page * pageSize;
        // If the start index exceeds the list length, do not try and fix the end index to the end.
        const endIndex = startIndex < length ? Math.min(startIndex + pageSize, length) : startIndex + pageSize;
        return `${startIndex + 1} - ${endIndex} de ${length}`;
      };

      return customPaginatorIntl;
  }