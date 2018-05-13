import React from 'react';
import { render, Simulate, fireEvent, wait } from 'react-testing-library';
import { MemoryRouter } from 'react-router';
import { Route } from "react-router-dom";
import { createMemoryHistory } from 'history';
import TimeRegistrationForm from './TimeRegistrationForm';
import { apiConfiguration } from '../configuration';
import { fetchTimeRegistration, saveTimeRegistration } from '../services/TimeRegistrationService';
import { fetchAllProjects } from '../services/ProjectService';
import fetchMock from 'fetch-mock';

const fakeProjects = [
    {
        id: 1,
        name: 'Project1',
        customerName: 'Customer1',
        dueDate: new Date(),
        hourlyRate: 45
    }, {
        id: 2,
        name: 'Project2',
        customerName: 'Customer2',
        dueDate: new Date(),
        hourlyRate: 42
    }
];

const fakeTimeRegistration = {
    id: 1,
    projectName: 'Project1',
    customerName: 'Customer1',
    date: new Date(),
    hoursWorked: 7.5,
    accounted: false
};

const newTimeRegistration = {
    id: 0,
    projectName: 'Project2',
    customerName: 'Customer2',
    date: new Date(),
    hoursWorked: 5,
    accounted: false
};

const setup = async(initialPath) => {
    const replacedInitialPath = initialPath.replace(':id', '1');
    const history = createMemoryHistory(replacedInitialPath);
    const renderedComponent = render(
        <MemoryRouter initialEntries={[replacedInitialPath]}>
            <Route path={initialPath} render={(props) => <TimeRegistrationForm {...props} 
                fetchTimeRegistration={fetchTimeRegistration} 
                saveTimeRegistration={saveTimeRegistration} 
                fetchAllProjects={fetchAllProjects}
                history={history} />} 
            />
        </MemoryRouter>
    );

    await wait();

    return renderedComponent;
}

describe('Time Registration Form Component', () => {
    beforeEach(() => {
        fetchMock.restore();

        fetchMock.getOnce(`${apiConfiguration.url}projects`, fakeProjects);
        fetchMock.getOnce(`${apiConfiguration.url}timeregistrations/1`, fakeTimeRegistration);
        fetchMock.postOnce(`${apiConfiguration.url}timeregistrations`, newTimeRegistration);
        fetchMock.putOnce(`${apiConfiguration.url}timeregistrations/1`, fakeTimeRegistration);
    });

    it('should render "New Time Registration" title when adding new time registration', async() => {
        const { queryByText } = await setup('/timeregistrations/create');

        const title = queryByText('New Time Registration');
        expect(title.innerHTML).toBe('New Time Registration');
    });

    it('should render "Edit Time Registration" title when editing existing time registration', async() => {
        const { queryByText } = await setup('/timeregistrations/edit/:id');

        const title = queryByText('Edit Time Registration');
        expect(title.innerHTML).toBe('Edit Time Registration');
    });

    it('should render a list of projects in a select element', async() => {
        const { getByTestId } = await setup('/timeregistrations/create');

        const selProjects = getByTestId('selProjects');
        expect(selProjects.options).toHaveLength(2);

        const project1Option = selProjects.options[0];
        expect(project1Option.text).toBe(`${fakeProjects[0].name} (${fakeProjects[0].customerName})`);
    });

    it('should create a new time registration when Save button is pressed', async() => {
        const { getByText } = await setup('/timeregistrations/create');

        const saveButton = getByText('Save');
        expect(saveButton.tagName.toLowerCase()).toBe('button');

        fireEvent(
            saveButton,
            new MouseEvent('click', {
                bubbles: true, // click events must bubble for React to see it
                cancelable: true,
            })
        );

        await wait(() => getByText('Save'));

        expect(fetchMock.lastCall()[0]).toBe(`${apiConfiguration.url}timeregistrations`);
        expect(fetchMock.lastCall()[1].method).toBe('POST');
    });
});