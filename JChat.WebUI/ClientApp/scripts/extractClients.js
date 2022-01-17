const { exec } = require('child_process');
const fs = require('fs');
const path = require('path');
const readline = require('readline');

const buildFileStr = (clients) => {
  const lines = [
    '// eslint-disable-next-line import/extensions,import/no-unresolved',
    "import * as ApiDeclaration from './web-api-client';",
    "import axios from '@/services/axios.service';",
    '',
  ];

  clients.forEach((client) => {
    lines.push(
      `export const ${client} = new ApiDeclaration.${client}Client('${process.env.JCHAT_API_BASE_URL}', axios);`
    );
  });

  lines.push('');

  return lines;
};

(async () => {
  const fileStream = fs.createReadStream(
    path.join(__dirname, '../src/api/web-api-client.ts')
  );

  const rl = readline.createInterface({
    input: fileStream,
    crlfDelay: Infinity,
  });

  const regExp = /^export class ([\w]+)Client implements I[\w]+Client\s{$/;
  const clients = [];

  for await (const line of rl) {
    const match = regExp.exec(line);

    if (match !== null) {
      clients.push(match[1]);
    }
  }

  const apiFilePath = path.join(__dirname, '../src/api/index.js');
  fs.writeFileSync(apiFilePath, buildFileStr(clients.sort()).join('\n'));
  exec(
    `yarn run prettier --write --config ${path.join(
      __dirname,
      '../.prettierrc'
    )} ${apiFilePath}`,
    (error, stdout, stderr) => {
      if (error) {
        console.error(`error: ${error.message}`);
      }
      if (stderr) {
        console.error(`stderr: ${stderr}`);
      }
    }
  );
})();
