import { http } from './http';

export interface PropertyListParams {
  name?: string;
  address?: string;
  priceFrom?: number;
  priceTo?: number;
  page?: number;
  pageSize?: number;
}

export interface PropertyListDto {
  idProperty: string;
  name: string;
  address?: string | null;
  price: number;
  image?: string | null;
  idOwner?: string | null;
}

export interface PropertyTraceDto {
  idPropertyTrace: string;
  dateSale?: string;
  name?: string;
  value?: string;
  tax?: number;
  idProperty?: string;
}; 

export type PropertyDetailDto = {
  idProperty: string;
  name?: string;
  address?: string;
  price?: number;
  images: string[]; // urls absolutas o relativas
  traces: PropertyTraceDto[];
  owners: OwnerDto[];
};

export type OwnerDto = {
  idOwner: string;
  name?: string;
  address?: string;
  photo?: string; // url
  birthday?: string;
};

export type PropertyGalleryProps = { images: string[]; initialIndex?: number };
export type PropertyProfileProps = { property: PropertyDetailDto };
export type OwnerProfileProps = { owners: OwnerDto[] };


export const fetchProperties = (params: PropertyListParams) =>
  http.get<PropertyListDto[]>('', { params }).then(r => r.data);

export const fetchPropertyById = (id: string) =>
  http.get<PropertyDetailDto>(`/${encodeURIComponent(id)}`).then(r => r.data);