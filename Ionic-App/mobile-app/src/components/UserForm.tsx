import React, { useState } from 'react';
import { IonInput, IonButton, IonItem, IonLabel, IonDatetime, IonToggle, IonSelect, IonSelectOption } from '@ionic/react';

interface UserFormProps {
  onSubmit: (userData: UserData) => void;
}

interface UserData {
  fullName: string;
  email: string;
  dob: string;
  subjectArea: string;
  marketingUpdates: boolean;
  correspondenceLanguage: boolean;
  gpsLocation: string;
}

const UserForm: React.FC<UserFormProps> = ({ onSubmit }) => {
  const [fullName, setFullName] = useState('');
  const [email, setEmail] = useState('');
  const [dob, setDob] = useState('');
  const [subjectArea, setSubjectArea] = useState('');
  const [marketingUpdates, setMarketingUpdates] = useState(false);
  const [correspondenceLanguage, setCorrespondenceLanguage] = useState(false);
  const [gpsLocation, setGpsLocation] = useState('');

  const handleSubmit = () => {
    const userData: UserData = {
      fullName,
      email,
      dob,
      subjectArea,
      marketingUpdates,
      correspondenceLanguage,
      gpsLocation
    };
    onSubmit(userData);
  };

  return (
    <form onSubmit={handleSubmit}>
      <IonItem>
        <IonLabel position="floating">Full Name</IonLabel>
        <IonInput value={fullName} onIonChange={e => setFullName(e.detail.value!)} required></IonInput>
      </IonItem>

      <IonItem>
        <IonLabel position="floating">Email</IonLabel>
        <IonInput type="email" value={email} onIonChange={e => setEmail(e.detail.value!)} required></IonInput>
      </IonItem>

      <IonItem>
        <IonLabel position="floating">Date of Birth</IonLabel>
        <IonLabel position="floating">' '</IonLabel>
        <IonDatetime displayFormat="MM/DD/YYYY" value={dob} onIonChange={e => setDob(e.detail.value!)} required></IonDatetime>
      </IonItem>

      <IonItem>
        <IonLabel position="floating">Subject Area</IonLabel>
        <IonSelect value={subjectArea} onIonChange={e => setSubjectArea(e.detail.value!)} required>
          <IonSelectOption value="Computer Science">Computer Science</IonSelectOption>
          <IonSelectOption value="Engineering">MBA Global</IonSelectOption>
          <IonSelectOption value="Engineering">Data Scinece</IonSelectOption>
          <IonSelectOption value="Engineering">AI</IonSelectOption>
          <IonSelectOption value="Engineering">EEE</IonSelectOption>
          {/* Add other subject areas */}
        </IonSelect>
      </IonItem>

      <IonItem>
        <IonLabel>Marketing Updates</IonLabel>
        <IonToggle checked={marketingUpdates} onIonChange={e => setMarketingUpdates(e.detail.checked)}></IonToggle>
      </IonItem>

      <IonItem>
        <IonLabel>Correspondence Language (Welsh)</IonLabel>
        <IonToggle checked={correspondenceLanguage} onIonChange={e => setCorrespondenceLanguage(e.detail.checked)}></IonToggle>
      </IonItem>

      {/* GPS location input can be added here */}

      <IonButton expand="block" type="submit">Submit</IonButton>
    </form>
  );
};

export default UserForm;
