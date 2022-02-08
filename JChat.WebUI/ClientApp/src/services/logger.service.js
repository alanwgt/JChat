import chalk from 'chalk';

import inspect from '@/utils/inspect.utils';

export const LogLevel = Object.freeze({
  TRACE: 1,
  DEBUG: 20,
  INFO: 30,
  WARN: 40,
  ERROR: 50,
  FATAL: 100,
});

const LogLevelData = Object.freeze({
  [LogLevel.TRACE]: { label: 'TRACE', fmt: chalk.white.bold },
  [LogLevel.DEBUG]: { label: 'DEBUG', fmt: chalk.magenta.bold },
  [LogLevel.INFO]: { label: 'INFO', fmt: chalk.blueBright.bold },
  [LogLevel.WARN]: { label: 'WARNING', fmt: chalk.yellow.bold },
  [LogLevel.ERROR]: { label: 'ERROR', fmt: chalk.red.bold },
  [LogLevel.FATAL]: { label: 'FATAL', fmt: chalk.redBright.bgBlack.bold },
});

let currentLogLevel =
  process.env.NODE_ENV === 'production' ? LogLevel.INFO : LogLevel.DEBUG;

export const setLogLevel = (level) => {
  currentLogLevel = level;
};

export const fmter =
  (level) =>
  (template, ...args) => {
    if (level < currentLogLevel) {
      return;
    }

    let str = '';

    for (let i = 0; i < template.length - 1; i += 1) {
      const arg = args[i];
      let strArg;

      if (typeof arg === 'undefined') strArg = '';
      else if (arg instanceof Error) strArg = inspect(arg);
      else if (typeof arg === 'string') strArg = arg;
      else if (
        typeof arg.hasOwnProperty === 'function' &&
        // eslint-disable-next-line no-prototype-builtins
        arg.hasOwnProperty('toString')
      )
        strArg = arg.toString();
      else strArg = inspect(arg);

      str += `${template[i]}${strArg}`;
    }

    if (template.length) str += template[template.length - 1];

    const data = LogLevelData[level];

    console.log(`[${data.fmt(data.label)}]: ${str}`);
  };

const Console = {
  trace: fmter(LogLevel.TRACE),
  debug: fmter(LogLevel.DEBUG),
  info: fmter(LogLevel.INFO),
  warn: fmter(LogLevel.WARN),
  error: fmter(LogLevel.ERROR),
  fatal: fmter(LogLevel.FATAL),
};

export default Console;
