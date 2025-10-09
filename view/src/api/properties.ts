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

export interface PropertyDetailDto {
  idProperty: string;
  name: string;
  address?: string | null;
  price: number;
  codeInternal?: string | null;
  year?: number | null;
  idOwner?: string | null;
  images?: string[] | null;
  traces?: any[] | null;
}

export const fetchProperties = (params: PropertyListParams) =>
  http.get<PropertyListDto[]>('', { params }).then(r => r.data);

export const fetchPropertyById = (id: string) =>
  http.get<PropertyDetailDto>(`/${encodeURIComponent(id)}`).then(r => r.data);