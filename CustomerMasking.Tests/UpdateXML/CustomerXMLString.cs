namespace CustomerMasking.Tests.UpdateXML
{
    public static class CustomerXMLString
    {
        public static string GetTestXML()
        {
            return @"
                <AnswersDefinition xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                  <Version>
                    <VersionNumber>1</VersionNumber>
                  </Version>
                  <PL>
                    <AP>
                      <N />
                      <AL>
                        <APS>
                          <T>Min profil</T>
                          <AL>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>Espen Øyen</DV>
                              </DVL>
                              <VL>
                                <V>7</V>
                              </VL>
                              <QT>CustomerManagingUserTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>ttttt</DV>
                              </DVL>
                              <VL>
                                <V>ttttt</V>
                              </VL>
                              <QT>CustomerAndInsuredPersonFirstNameTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>ttttt</DV>
                              </DVL>
                              <VL>
                                <V>ttttt</V>
                              </VL>
                              <QT>CustomerAndInsuredPersonLastNameTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>SecondFirstNameTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>SecondLastNameTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>t</DV>
                              </DVL>
                              <VL>
                                <V>t</V>
                              </VL>
                              <QT>AddrLine1Tag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>0789</DV>
                              </DVL>
                              <VL>
                                <V>0789</V>
                              </VL>
                              <QT>AddrPostCodeTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>OSLO</DV>
                              </DVL>
                              <VL>
                                <V>OSLO</V>
                              </VL>
                              <QT>AddrLine3Tag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>Nei</DV>
                              </DVL>
                              <VL>
                                <V>No</V>
                              </VL>
                              <QT>SeperatePostalAddressTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>MailingZipCode</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>MailingPostalAddress</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>MailingAddress</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>01025012345</DV>
                              </DVL>
                              <VL>
                                <V>01025012345</V>
                              </VL>
                              <QT>CustomerAndInsuredPersonSocialSecurityTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>01.02.1950</DV>
                              </DVL>
                              <VL>
                                <V>01.02.1950</V>
                              </VL>
                              <QT>CustomerAndInsuredPersonDOBTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>123333</DV>
                              </DVL>
                              <VL>
                                <V>123333</V>
                              </VL>
                              <QT>GenericTelephoneNumberTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>Mobil</DV>
                              </DVL>
                              <VL>
                                <V>Mobile</V>
                              </VL>
                              <QT>TelephoneNumberTypeTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>nooot@not.not</DV>
                              </DVL>
                              <VL>
                                <V>nooot@not.not</V>
                              </VL>
                              <QT>EmailAndUsernameTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>Faktura - 1 termin (ny)</DV>
                              </DVL>
                              <VL>
                                <V>NewExternalInvoice1</V>
                              </VL>
                              <QT>PreferredPaymentMethodTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>DonationOrganisationTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>Nei</DV>
                              </DVL>
                              <VL>
                                <V>No</V>
                              </VL>
                              <QT>DonationOrganisationMembershipTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>Nei</DV>
                              </DVL>
                              <VL>
                                <V>No</V>
                              </VL>
                              <QT>CustomerHasSetUseEndDateAlignmentTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>CustomerEndDateAlignmentDayTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>CustomerEndDateAlignmentMonthTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>FreeTextTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>Automatisk</DV>
                              </DVL>
                              <VL>
                                <V>Automatic</V>
                              </VL>
                              <QT>PolicyRenewalModeTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>PreferredCollectionDayTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>Annet offentlig sektor</DV>
                              </DVL>
                              <VL>
                                <V>Annet offentlig sektor</V>
                              </VL>
                              <QT>OccupationTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>0-1</DV>
                              </DVL>
                              <VL>
                                <V>0-1</V>
                              </VL>
                              <QT>EducationTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>Nei</DV>
                              </DVL>
                              <VL>
                                <V>No</V>
                              </VL>
                              <QT>PaperInvoiceTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>StandardCommissionType</DV>
                              </DVL>
                              <VL>
                                <V>StandardCommissionType</V>
                              </VL>
                              <QT>DisplayDefaultCommissionTypeTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL>
                                <DV>StandardCommissionType</DV>
                              </DVL>
                              <VL>
                                <V>4</V>
                              </VL>
                              <QT>CustomerCommissionTypeTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>PreviousInsurerTag</QT>
                            </AWT>
                          </AL>
                        </APS>
                        <APS>
                          <T>Bankinformasjon</T>
                          <AL>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>QAndAPaymentDetailsHasDetails</QT>
                            </AWT>
                          </AL>
                        </APS>
                        <APS>
                          <T>Nåværende Bankinformasjon</T>
                          <AL>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>BankAccountNumberWithAsterisksTag</QT>
                            </AWT>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>BankEnterNewDetailsTag</QT>
                            </AWT>
                          </AL>
                        </APS>
                        <APS>
                          <T>Ny Bankinformasjon</T>
                          <AL>
                            <AWT>
                              <VAL>true</VAL>
                              <DVL />
                              <VL />
                              <QT>BankAccountNumberTag</QT>
                            </AWT>
                          </AL>
                        </APS>
                      </AL>
                    </AP>
                  </PL>
                </AnswersDefinition>";
        }
    }
}
