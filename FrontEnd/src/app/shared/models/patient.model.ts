export interface Patient {
  idPatient: number ,
  documentNumber: string,
  names: string,
  lastNames: string,
  email: string,
  phone: string,
  birthDate: string,
  documentType: string,
  afiliation: string,
}

export interface DetailPatient {
  idPatient?: number,
  idTypeDocument: number,
  idAffiliationType: number,
  documentNumber: string,
  names: string,
  lastNames: string,
  email: string,
  phone: string,
  birthDate: string | any
}